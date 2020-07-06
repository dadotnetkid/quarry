using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class TransactionViewModel
    {

        public int? ItemId { get; set; }
        public int? TransactionDetailId { get; set; }
        public string TransactionId { get; set; }
        public int? PermiteeId { get; set; }
        public int? TransactionVehicleId { get; set; }
        public int? VehicleId { get; set; }
        public int? FacilitiesId { get; set; }
        public int? TransactionFacilitiesId { get; set; }
        public int? TransactionTypeId { get; set; }
        public List<string> ProgramOfWorkIds { get; set; }
        public List<string> QuarryIds{ get; set; }

        public int? SagId { get; set; }

        public int? TransactionSagId { get; set; }

        public bool? isDelivery { get; set; }
        public int QuarriesInTransactionId { get; set; }
    }
}
