using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Repository;

namespace Models
{
    public partial class TransactionSags
    {
        public decimal TotalCost => (this.UnitCost ?? 0.0M) * (this.Quantity ?? 0);
        public string CostAndQuantity => this.UnitCost?.ToString("n2") + "/" + this.Sags?.UnitMeasurements?.UnitMeasure;
        public string QuantityAndUnitMeasure => (this.Quantity ?? 0) + " " + this.Sags?.UnitMeasurements?.UnitMeasure;

        public decimal? ProductionQty => new UnitOfWork().ProductionsRepo
            .Fetch(m => m.SagId == SagId && m.TransactionId == TransactionId).Sum(x => (x.Quantity));
    }
}
