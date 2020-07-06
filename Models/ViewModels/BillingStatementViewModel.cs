using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Repository;

namespace Models.ViewModels
{
    public class BillingStatementViewModel
    {
        public string TransactionId { get; set; }
        private UnitOfWork unitOfWork = new UnitOfWork();

        public Transactions Transactions => unitOfWork.TransactionsRepo.Find(m => m.Id == TransactionId);
        public List<TransactionDetails> GovernorsBusinessPermitFee
        {
            get
            {
                var x = unitOfWork.TransactionDetailsRepo.Get(
                    m => m.TransactionId == TransactionId && m.Items.Categories.CategoryName == "Governor's Business Permit Fees").ToList();

                return x;
            }
        }

        public List<TransactionDetails> GovernorsAccreditationFees
        {
            get
            {
                return unitOfWork.TransactionDetailsRepo.Get(
                    m => m.TransactionId == TransactionId && m.Items.Categories.CategoryName == "Governor's Accreditation Fees").ToList();
            }
        }

        private decimal totalGovernorAccreditationFees()
        {
            // var a = GovernorsAccreditationFees.Sum(m => m.TotalCost) ?? 0;
            var b = VehicleTypes.Sum(m => m.Cost) ?? 0;
            var c = TaxOnExcessSAGVolume.Sum(m => m.TotalCost) ?? 0;
            var d = TransactionFacilities.Sum(m => m.Cost) ?? 0;
            var e = Transactions?.Surcharge ?? 0;
            var f = Transactions?.Interest ?? 0;
            var g = Transactions?.PenaltyCost ?? 0;
            return b + c + d + e + f + g;
        }
        public decimal GrandTotal
        {
            get
            {
                var a = GovernorsAccreditationFees.Sum(m => m.TotalCost) ?? 0;
                var b = VehicleTypes.Sum(m => m.Cost) ?? 0;
                var c = TaxOnExcessSAGVolume.Sum(m => m.TotalCost) ?? 0;
                var d = TransactionFacilities.Sum(m => m.Cost) ?? 0;
                var e = Transactions?.Surcharge ?? 0;
                var f = Transactions?.Interest ?? 0;
                var g = Transactions?.PenaltyCost ?? 0;
                var h = TransactionSags?.Sum(m => m.TotalCost) ?? 0;
                var i = GovernorsBusinessPermitFee.Sum(m => m.TotalCost) ?? 0;
                return a + b + c + d + e + f + g + h+i;
            }
        }
        public List<TransactionPenalties> TransactionPenalties => this.Transactions?.TransactionPenalties.ToList();

        public decimal TotalGovernorAccreditationFees => totalGovernorAccreditationFees();

        public List<VehicleTypes> VehicleTypes
        {
            get
            {

                var vehicleTypes = new List<VehicleTypes>();
                foreach (var i in unitOfWork.VehicleTypesRepo.Get())
                {
                    if (TransactionVehicles.Any(m => m.Vehicles.VehicleTypeId == i.Id))
                        vehicleTypes.Add(new Models.VehicleTypes()
                        {
                            VehicleTypeName = i.VehicleTypeName,
                            ListVehicles = string.Join(", ", TransactionVehicles.Where(m => m.Vehicles.VehicleTypeId == i.Id).Select(x => x.Vehicles.PlateNo)),
                            Cost = TransactionVehicles.Where(m => m.Vehicles.VehicleTypeId == i.Id).Sum(m => m.Cost)
                        });
                }

                return vehicleTypes;
            }
        }

        public List<TransactionVehicles> TransactionVehicles
        {
            get { return unitOfWork.TransactionVehiclesRepo.Get(m => m.TransactionId == TransactionId, includeProperties: "Vehicles").ToList(); }
        }
        public List<TransactionFacilities> TransactionFacilities
        {
            get { return unitOfWork.TransactionFacilitiesRepo.Get(m => m.TransactionId == TransactionId).ToList(); }
        }

        public IEnumerable<TransactionDetails> TaxOnExcessSAGVolume
        {
            get
            {
                return unitOfWork.TransactionDetailsRepo.Get(
                    m => m.TransactionId == TransactionId && m.Items.Categories.CategoryName == "Tax On Excess Sag Volume").ToList();
            }
        }

        public List<TransactionSags> TransactionSags
        {
            get { return unitOfWork.TransactionSagsRepo.Get(m => m.TransactionId == TransactionId).ToList(); }
        }

        public decimal? TotalVehicleandFacilities => (this.TransactionVehicles.Sum(m => m.Cost) ?? 0.0M) +
                                                     (this.TransactionFacilities.Sum(m => m.Cost) ?? 0.0M);
    }


}
