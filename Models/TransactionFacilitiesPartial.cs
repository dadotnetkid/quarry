using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
  public partial class TransactionFacilities
    {
        public string isRenewStatus => isRenew == true ? "Renew" : "Old";
    }
}
