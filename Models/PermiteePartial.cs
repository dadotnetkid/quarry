using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Repository;
using Models.ViewModels;

namespace Models
{
    [MetadataType(typeof(PermiteeMeta))]
    public partial class Permitees
    {
       

        public List<int> QuarrySites { get; set; }
        public string _QuarySites => string.Join(", ", this.Quarries.Select(x => x.QuarryName));

        public List<ProductionLedgerReport> ProductionLedgerReports { get; set; }
        private SumProductionLedgerReport _sumProductionLedgerReport;

        public SumProductionLedgerReport SumProductionLedgerReport
        {
            get
            {
                if (_sumProductionLedgerReport == null)
                    _sumProductionLedgerReport = new SumProductionLedgerReport()
                    {
                        ProductionBoulders = ProductionLedgerReports.Sum(m => m.ProductionBoulders),
                        ProductionBouldersAmount = ProductionLedgerReports.Sum(m => m.ProductionBouldersAmount),
                        ProductionCoarse = ProductionLedgerReports.Sum(m => m.ProductionCoarse),
                        ProductionCoarseAmount = ProductionLedgerReports.Sum(m => m.ProductionCoarseAmount),
                        ProductionCrushed = ProductionLedgerReports.Sum(m => m.ProductionCrushed),
                        ProductionCrushedAmount = ProductionLedgerReports.Sum(m => m.ProductionCrushedAmount),
                        ProductionFined = ProductionLedgerReports.Sum(m => m.ProductionFined),
                        ProductionFinedAmount = ProductionLedgerReports.Sum(m => m.ProductionFinedAmount),
                        ProductionMixed = ProductionLedgerReports.Sum(m => m.ProductionMixed),
                        ProductionMixedAmount = ProductionLedgerReports.Sum(m => m.ProductionMixedAmount),
                        ProductionOrdinaryEarth = ProductionLedgerReports.Sum(m => m.ProductionOrdinaryEarth),
                        ProductionOrdinaryEarthAmount =
                            ProductionLedgerReports.Sum(m => m.ProductionOrdinaryEarthAmount),
                    };

                return _sumProductionLedgerReport;
            }
            set { _sumProductionLedgerReport = value; }
        }

        public string CompanyAndPermitteeName => this.CompanyName + Environment.NewLine + this.FullName;


        public List<Transactions> BillingStatements => new UnitOfWork().TransactionsRepo.Fetch(x => x.PermiteeId == Id && x.TransactionSags.Any()).ToList();

    }


    public class PermiteeMeta
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Middle Name is required")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Address Line is required")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [NotMapped]
        public List<int> QuarrySites { get; set; }
        [NotMapped]
        public string _QuarySites { get; set; }
    }
}
