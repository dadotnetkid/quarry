using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Web;
using DevExpress.XtraReports.UI;
using Microsoft.AspNet.Identity;

namespace Helpers
{
    public partial class rptProductionLedgerList : DevExpress.XtraReports.UI.XtraReport
    {
        public rptProductionLedgerList()
        {
            InitializeComponent();
        }

        private void xrLabel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel1.Text = $"[{HttpContext.Current?.User?.Identity?.GetUserName()}] Quarry Data Management System";
        }
    }
}
