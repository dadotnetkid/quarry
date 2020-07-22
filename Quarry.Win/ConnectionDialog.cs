using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.ConnectionUI;
using Quarry.Win.Properties;
using DataProvider = Microsoft.Data.ConnectionUI.DataProvider;

namespace Quarry.Win
{
    public class ConnectionDialog
    {

        public static string ShowDialog()
        {
            var cd = new DataConnectionDialog();
            DataSource.AddStandardDataSources(cd);
            cd.SelectedDataSource = DataSource.SqlDataSource;
            cd.ConnectionString = Settings.Default.ConnectionString;
            cd.SelectedDataProvider = DataProvider.SqlDataProvider;

            if (DataConnectionDialog.Show(cd) == System.Windows.Forms.DialogResult.Cancel)
                return Settings.Default.ConnectionString;
            Settings.Default.ConnectionString = cd.ConnectionString;
            Settings.Default.Save();
            DataSources.ConnectionString = cd.ConnectionString;
            return cd.ConnectionString;
        }

        private static void Cd_Shown(object sender, EventArgs e)
        {
        }
    }
}
