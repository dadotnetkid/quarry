using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class TransactionPenalties
    {
        public string PenaltyAmount => this.Amount < 0 ? Math.Abs(this.Amount ?? 0).ToString("(00.00 Php)") : (this.Amount ?? 0).ToString("00.00 Php");
    }
}
