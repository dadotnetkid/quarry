using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Helpers;
using Models;
using Models.Repository;
using Models.ViewModels;

namespace Quarry.Win.Trnsctns
{
    public partial class UCTransactions : DevExpress.XtraEditors.XtraUserControl
    {
        public UCTransactions()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                var transaction = unitOfWork.TransactionsRepo.Get();
                this.transactionsBindingSource.DataSource = transaction;
                this.Detail(transaction.FirstOrDefault());
            }
            catch (Exception e)
            {

            }
        }

        private void Detail(Transactions transactions)
        {
            try
            {
                if (transactions == null)
                    return;
                UnitOfWork unitOfWork = new UnitOfWork();
                transactions = unitOfWork.TransactionsRepo.Find(x => x.Id == transactions.Id);
                this.txtTransNo.Text = transactions.TransactionNumber;
                this.txtOR.Text = transactions.OfficialReceipt;
                this.txtPermitteeName.Text = transactions.Permitees?.FullName;
                this.txtCompanyName.Text = transactions.Permitees?.CompanyName;
                this.txtTransactionType.Text = transactions.TransactionTypes?.TransactionType;
                this.txttTotalAmount.Text = transactions.TransactionTotal?.ToString("n2");
                this.txtDate.EditValue = transactions.FilingDate;
                this.lblTransNo.Text = transactions.TransactionNumber + " - " + transactions.Permitees?.CompanyName;
                btnPayment.Enabled = true;
                btnPreview.Enabled = true;
                if (!transactions.Billings.Any())
                {
                    btnPreview.Enabled = false;
                }
                // 
                if (transactions.Billings.Any())
                {
                    btnPayment.Enabled = false;
                }
                // 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var search = txtSearch.Text;
                UnitOfWork unitOfWork = new UnitOfWork();
                IQueryable<Transactions> transactions = unitOfWork.TransactionsRepo.Fetch();
                if (transactions.Select(x => new { x.Permitees.CompanyName }).Any(x => x.CompanyName.Contains(search)))
                    transactions = transactions.Where(x => x.Permitees.CompanyName.Contains(search));
                if (transactions.Any(x => x.OfficialReceipt.Contains(search)))
                    transactions = transactions.Where(x => x.OfficialReceipt.Contains(search));
                if (transactions.Any(x => x.TransactionNumber.Contains(search)))
                    transactions = transactions.Where(x => x.TransactionNumber.Contains(search));
                this.transactionsBindingSource.DataSource = transactions.ToList();
                Detail(transactions.FirstOrDefault());
                /*if (transactions.Select(x => new { FullName = x.Permitees.FirstName + " " + x.Permitees.MiddleName[0] + ". " + x.Permitees.LastName }).Any(x => x.FullName.Contains(search)))
                    transactions=*/
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TransactionGridView.GetFocusedRow() is Transactions item)
                {
                    frmAddEditBilling frmAddEditBilling = new frmAddEditBilling(item, MethodType.Add);
                    frmAddEditBilling.ShowDialog();
                    Detail(new UnitOfWork().TransactionsRepo.Find(x => x.Id == item.Id));
                }
            }
            catch (Exception exception)
            {

            }
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void TransactionGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (sender is GridView grid)
            {
                if (grid.GetFocusedRow() is Transactions item)
                {
                    Detail(new UnitOfWork().TransactionsRepo.Find(x => x.Id == item.Id));
                }
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (TransactionGridView.GetFocusedRow() is Transactions item)
                {
                    BillingStatementViewModel billingStatementViewModel = new BillingStatementViewModel()
                    {
                        TransactionId = item.Id
                    };
                    var billing = new UnitOfWork().BillingsRepo.Find(x => x.TransactionId == item.Id);
                    billing.AmountInWord = "**" + StringExtensions.NumberToWords(billing.Amount.ToString("##.##00")) + "**";
                    billing.BillingStatementViewModels =
                        new List<BillingStatementViewModel>() { billingStatementViewModel };
                    frmReportViewer frmReportViewer = new frmReportViewer(new rptOfficialReceipt()
                    {
                        DataSource = new List<Billings>() { billing }
                    });
                    frmReportViewer.ShowDialog();
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, exception.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
