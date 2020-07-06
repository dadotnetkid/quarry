using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Repository;

namespace Models.ViewModels
{
    public class PermitteeBillingMasterlistViewModel
    {

        public List<Permitees> Permitees { get; set; }
        public string TransactionId { get; set; }
    }
   
}
