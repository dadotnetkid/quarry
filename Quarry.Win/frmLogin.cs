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
using Models.Startup;
using Quarry.Win.Properties;

namespace Quarry.Win
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        private bool isClosed;

        public frmLogin()
        {
            InitializeComponent();
            txtToday.Text = DateTime.Now.ToString();
        }



        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (Settings.Default.RememberMe)
            {
                txtPassword.Text = Settings.Default.Password;
                txtUserName.Text = Settings.Default.UserName;
                chkRemember.Checked = Settings.Default.RememberMe;
            }
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            unitOfWork = new UnitOfWork();

            ApplicationUserManager userManager =
                new ApplicationUserManager(new UserStores(ModelDb.Create()));
            var user = await userManager.FindByNameAsync(txtUserName.Text);
            if (user == null)
            {
                MessageBox.Show("Invalid UserName", "Invalid UserName", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var res = userManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, txtPassword.Text);
            if (res != Microsoft.AspNet.Identity.PasswordVerificationResult.Success)
            {
                MessageBox.Show("Invalid Password", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                if (chkRemember.Checked)
                {
                    Settings.Default.UserName = txtUserName.Text;
                    Settings.Default.Password = txtPassword.Text;
                    Settings.Default.RememberMe = chkRemember.Checked;
                    Settings.Default.Save();
                }
                else
                {
                    Settings.Default.UserName = string.Empty;
                    Settings.Default.Password = string.Empty;
                    Settings.Default.RememberMe = false;
                    Settings.Default.Save();
                }
            User.UserId = user.Id;
            isClosed = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void btnServerSetup_Click(object sender, EventArgs e)
        {
            ConnectionDialog.ShowDialog();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!this.isClosed)
                Application.ExitThread();
        }
    }
}