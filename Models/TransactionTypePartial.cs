using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class TransactionTypes
    {
    }

    public enum TransactionType
    {
        New = 1,
        Renew = 2,
        AdditionalSag = 3

    }
}
