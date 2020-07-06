using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class QuarriesInTransactions
    {
        public string QuarrySitesDistribution => this.Sags?.Sag + "-" + this.Quarries?.QuarryName + " " + this.Percentage + "%";
    }
}
