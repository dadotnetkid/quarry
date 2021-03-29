using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helpers;
using Models.Repository;
using Models.ViewModels;

namespace Win.test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dateFrom = new DateTime(2020, 06, 1);
            var dateTo = new DateTime(2020, 06, DateTime.DaysInMonth(2020, 6)).AddHours(23).AddMinutes(59).AddSeconds(59); 
            UnitOfWork unitOfWork = new UnitOfWork();
            var permittees = unitOfWork.PermiteesRepo.Fetch(includeProperties: "Productions");

            var res = permittees.Where(x => x.Productions.Any(m => (m.DateCreated >= dateFrom && m.DateCreated <= dateTo))).ToList();
            decimal prodTotal = 0;
            foreach (var permittee in res)
            {
                permittee.Productions = unitOfWork.ProductionsRepo.Get(m => (m.DateCreated >= dateFrom && m.DateCreated <= dateTo) && m.Transactions.PermiteeId == permittee.Id).ToList();
                // var totalExtraction = permittee.Productions.Sum(x => (x.Quantity ?? 0) * (x.Sags.UnitCost ?? 0));


                //for accounting
                var transaction = unitOfWork.TransactionsRepo.Fetch(m =>
                    m.Productions.Any(x => x.DateCreated >= dateFrom && x.DateCreated <= dateTo) && m.PermiteeId == permittee.Id);
                foreach (var t in transaction.ToList())
                {
                    foreach (var q in unitOfWork.ProductionsRepo.Fetch()
                        .Where(x => (x.DateCreated >= dateFrom && x.DateCreated <= dateTo) &&
                                    x.Transactions.PermiteeId == permittee.Id && x.TransactionId == t.Id).GroupBy(x => x.QuarriesId))
                    {
                        var quarryId = q.Key?.ToInt();
                        var prod = unitOfWork.ProductionsRepo.Fetch()
                            .Where(x => (x.DateCreated >= dateFrom && x.DateCreated <= dateTo) &&
                                        x.Transactions.PermiteeId == permittee.Id && x.QuarriesId == quarryId && x.TransactionId == t.Id);

                        prodTotal += prod?.Sum(x => (x.Quantity ?? 0) * (x.Sags.UnitCost ?? 0)) ?? 0;
                    }
                }
                //x.QuarriesId == quarryId && x.TransactionId == t.Id).ToList();

            }
            dataGridView1.Rows.Add(res.Sum(x => x.Productions?.Sum(m => (m.Quantity ?? 0) * (m.Sags?.UnitCost ?? 0))), prodTotal);
        }
    }
}
