using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Quarry.Win.Trnsctns;

namespace Quarry.Win
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Main()
        {
            frmLogin frm = new frmLogin();
            frm.ShowDialog();
            InitializeComponent();
            this.pnlMain.Controls.Clear();
            this.pnlMain.Controls.Add(new UCTransactions(){Dock=DockStyle.Fill});
        }

        private void btnBillings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.pnlMain.Controls.Clear();
            this.pnlMain.Controls.Add(new UCTransactions(){ Dock = DockStyle.Fill });
        }
    }
}
