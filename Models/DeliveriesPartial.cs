using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Repository;

namespace Models
{
    [MetadataType(typeof(ProductionsMeta))]
    public partial class Productions
    {
        private string _plateNo;

        //   public string Destination => new UnitOfWork().TownsRepo.Fetch(m => m.Id == DestinationId).FirstOrDefault()?.Name;
        public string Origin => new UnitOfWork().TownsRepo.Fetch(m => m.Id == OriginId).FirstOrDefault()?.Name;
        public decimal RemainingSagQuantity { get; set; }

        public bool? IsDelivery { get; set; }

        public string PlateNo
        {
            get
            {
                if (_plateNo == null)
                    _plateNo = this.Vehicles?.PlateNo;
                return _plateNo;
            }
            set => _plateNo = value;
        }


        public decimal? OrdinaryEarth => SagId == 1 ? Quantity : 0;
        public decimal? OrdinaryAmount => SagId == 1 ? this.Sags?.UnitCost * Quantity : 0.0M;

        public decimal? Mixed => SagId == 4 ? Quantity : 0;
        public decimal? MixedAmount => SagId == 4 ? this.Sags?.UnitCost * Quantity : 0.0M;
        public decimal? FineScreen => SagId == 5 ? Quantity : 0;
        public decimal? FineScreenAmount => SagId == 5 ? this.Sags?.UnitCost * Quantity : 0.0M;
        public decimal? Coarse => SagId == 8 ? Quantity : 0;
        public decimal? CoarseAmount => SagId == 8 ? this.Sags?.UnitCost * Quantity : 0.0M;
        public decimal? Boulders => SagId == 6 ? Quantity : 0;
        public decimal? BouldersAmount => SagId == 6 ? this.Sags?.UnitCost * Quantity : 0.0M;
        public decimal? CrushedRocks => SagId == 7 ? Quantity : 0;
        public decimal? CrushedRocksAmount => SagId == 7 ? this.Sags?.UnitCost * Quantity : 0.0M;

        public decimal? Total => OrdinaryEarth + Mixed + FineScreen + Coarse + Boulders + CrushedRocks;
        public decimal? TotalAmount => OrdinaryAmount + MixedAmount + FineScreenAmount + CoarseAmount + BouldersAmount + CrushedRocksAmount;
    }

    public class ProductionsMeta
    {
        [NotMapped]
        public string Destination { get; set; }
        [NotMapped]
        public string Origin { get; set; }
        [NotMapped]
        public int RemainingSagQuantity { get; set; }
    }
}
