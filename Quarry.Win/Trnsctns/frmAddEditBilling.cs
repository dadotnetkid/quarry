using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Helpers;
using Models;
using Models.Repository;

namespace Quarry.Win.Trnsctns
{
    public partial class frmAddEditBilling : DevExpress.XtraEditors.XtraForm
    {
        private Transactions transactions;
        private MethodType methodType;

        public frmAddEditBilling(Transactions transactions, MethodType methodType)
        {
            InitializeComponent();
            this.transactions = transactions;
            this.methodType = methodType;
            this.Detail(transactions);
        }

        private void Detail(Transactions item)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            item = unitOfWork.TransactionsRepo.Find(x => x.Id == item.Id);
            this.dtPaymentDate.EditValue = item.Billings?.FirstOrDefault()?.DateCreated ?? DateTime.Now;
            this.cboTransNo.Text = item.TransactionNumber;
            this.txtCompName.Text = item.Permitees.CompanyAndPermitteeName;
            this.txtTotal.Text = item.TransactionTotal?.ToString("n2");
            this.txtOrNo.Text = item.OfficialReceipt;
            this.txtAmountPaid.EditValue = item.Billings?.FirstOrDefault()?.Amount;
        }

        private void Init()
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();
            }
            catch (Exception e)
            {

            }
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void Save()
        {
            try
            {

                if (MessageBox.Show("Do you want to submit this?", "Submit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                UnitOfWork unitOfWork = new UnitOfWork();
                var item = unitOfWork.BillingsRepo.Find(x => x.TransactionId == transactions.Id);

                if (item == null)
                {
                    unitOfWork.BillingsRepo.Insert(new Billings()
                    {
                        Amount = txtAmountPaid.EditValue?.ToDecimal() ?? 0,
                        TransactionId = transactions.Id,
                        CreatedBy = User.UserId,
                        DateCreated = DateTime.Now,

                    });
                }
                else
                {
                    item.Amount = txtAmountPaid?.ToDecimal() ?? 0;
                    item.DateModified = DateTime.Now;
                    item.LastModifiedBy = User.UserId;
                }
                unitOfWork.Save();
                unitOfWork = new UnitOfWork();
                var trans = unitOfWork.TransactionsRepo.Find(x => x.Id == transactions.Id);
                trans.OfficialReceipt = txtOrNo.Text ;
                unitOfWork.Save();
                Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}