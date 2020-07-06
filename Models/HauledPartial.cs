using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class Hauleds
    {
        public decimal? Cost => this.Sags.UnitCost * this.Quantity;



        public decimal? OrdinaryEarth => SagId == 1 ? Quantity : 0;
        public decimal? OrdinaryAmount => SagId == 1 ? this.Sags?.UnitCost * Quantity : 0.0M;

        public decimal? Mixed => SagId == 4 ? Quantity : 0;
        public decimal? MixedAmount => SagId == 4 ? this.Sags?.UnitCost * Quantity : 0.0M;
        public decimal? FineScreen => SagId == 5 ? Quantity : 0;
        public decimal? FineScreenAmount
        {
            get
            {
                var res= SagId == 5 ? this.Sags?.UnitCost * Quantity : 0.0M;
                return res;
            }
        }

        public decimal? Coarse => SagId == 8 ? Quantity : 0;
        public decimal? CoarseAmount => SagId == 8 ? this.Sags?.UnitCost * Quantity : 0.0M;
        public decimal? Boulders => SagId == 6 ? Quantity : 0;
        public decimal? BouldersAmount => SagId == 6 ? this.Sags?.UnitCost * Quantity : 0.0M;
        public decimal? CrushedRocks => SagId == 7 ? Quantity : 0;
        public decimal? CrushedRocksAmount => SagId == 7 ? this.Sags?.UnitCost * Quantity : 0.0M;

        public decimal? Total => OrdinaryEarth + Mixed + FineScreen + Coarse + Boulders + CrushedRocks;
        public decimal? TotalAmount => OrdinaryAmount + MixedAmount + FineScreenAmount + CoarseAmount + BouldersAmount + CrushedRocksAmount;
    }
}
