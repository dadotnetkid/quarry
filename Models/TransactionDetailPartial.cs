using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class TransactionDetails
    {
        public decimal? TotalCost => this.UnitCost * this.Quantity;
        public string CostAndQuantity => this.UnitCost.ToString("n2") + "/" + this.Items?.UnitMeasurements?.UnitMeasure;
        public int? QuantityValue => Quantity == null ? 1 : Quantity;
        public string QuantityAndUnitMeasure => this.QuantityValue + " " + this.Items?.UnitMeasurements.UnitMeasure;

        

    }


    public class TransactionDetailsMeta
    {

    }
}
