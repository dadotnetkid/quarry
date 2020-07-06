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
using Models.Repository;
using Helpers;

namespace Win
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            foreach (var i in unitOfWork.ProductionsRepo.Get(m=>m.SagId>8))
            {
                var sag = unitOfWork.SagsRepo.Find(x => x.Id == i.SagId);

                var id = sag.Sag.ToInt();
                if (id == 0)
                    continue;
                var sagId = unitOfWork.SagsRepo.Find(x => x.Id == id);
                i.SagId = sagId.Id;
                unitOfWork.Save();
            }
        }
    }
}