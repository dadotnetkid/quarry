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
using DevExpress.XtraReports.UI;

namespace Quarry.Win
{
    public partial class frmReportViewer : DevExpress.XtraEditors.XtraForm
    {
        public frmReportViewer(XtraReport xtraReport)
        {
            InitializeComponent();
            xtraReport.CreateDocument();
            this.ReportViewer.DocumentSource = xtraReport;
        }
    }
}