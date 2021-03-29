using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{

    public class SummaryProductionReport
    {
        private SumProductionLedgerReport _sumProductionLedgerReport;
        public List<Permitees> Permitees { get; set; }
        public SumProductionLedgerReport SumProductionLedgerReport
        {
            get
            {
                if (_sumProductionLedgerReport == null)
                    _sumProductionLedgerReport = new SumProductionLedgerReport()
                    {
                        OrdinaryEarth = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.OrdinaryEarth)),
                        OrdinaryEarthAmount = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.OrdinaryEarthAmount)),
                        Boulders = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.Boulders)),
                        BouldersAmount = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.BouldersAmount)),
                        Coarse = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.Coarse)),
                        CoarseAmount = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.CoarseAmount)),
                        Crushed = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.Crushed)),
                        CrushedAmount = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.CrushedAmount)),
                        Fined = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.Fined)),
                        FinedAmount = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.FinedAmount)),
                        Mixed = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.Mixed)),
                        MixedAmount = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.MixedAmount)),
                        ProductionBoulders = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.ProductionBoulders)),
                        ProductionBouldersAmount = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.ProductionBouldersAmount)),
                        ProductionCoarse = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.ProductionCoarse)),
                        ProductionCoarseAmount = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.ProductionCoarseAmount)),
                        ProductionCrushed = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.ProductionCrushed)),
                        ProductionCrushedAmount = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.ProductionCrushedAmount)),
                        ProductionFined = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.ProductionFined)),
                        ProductionFinedAmount = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.ProductionFinedAmount)),
                        ProductionMixed = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.ProductionMixed)),
                        ProductionMixedAmount = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.ProductionMixedAmount)),
                        ProductionOrdinaryEarth = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.ProductionOrdinaryEarth)),
                        ProductionOrdinaryEarthAmount = Permitees.Sum(x => x.ProductionLedgerReports.Sum(m => m.ProductionOrdinaryEarthAmount)),
                    };

                return _sumProductionLedgerReport;
            }
            set { _sumProductionLedgerReport = value; }
        }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string ReportDate => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(Month ?? 1)) + " " + Year;
    }
    public class ProductionLedgerReport
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<int?> PermitteeTypeId { get; set; }
        public int? PermitteeId { get; set; }
        public bool? isSubmitted { get; set; }
        public Permitees Permitees { get; set; }
        public string ReportDate { get; set; }
        public int Month { get; set; }
        public int? Year { get; set; }

        public Quarries Quarries { get; set; }
        public decimal? OrdinaryEarthAmount { get; set; }
        public decimal? OrdinaryEarth { get; set; }
        public decimal? MixedAmount { get; set; }
        public decimal? Mixed { get; set; }
        public decimal? FinedAmount { get; set; }
        public decimal? Fined { get; set; }
        public decimal? CoarseAmount { get; set; }
        public decimal? Coarse { get; set; }
        public decimal? BouldersAmount { get; set; }
        public decimal? Boulders { get; set; }
        public decimal? CrushedAmount { get; set; }
        public decimal? Crushed { get; set; }
        //production
        public decimal? ProductionOrdinaryEarth { get; set; }
        public decimal? ProductionOrdinaryEarthAmount { get; set; }
        public decimal? ProductionMixed { get; set; }
        public decimal? ProductionMixedAmount { get; set; }
        public decimal? ProductionFined { get; set; }
        public decimal? ProductionFinedAmount { get; set; }
        public decimal? ProductionCoarse { get; set; }
        public decimal? ProductionCoarseAmount { get; set; }
        public decimal? ProductionBoulders { get; set; }
        public decimal? ProductionBouldersAmount { get; set; }
        public decimal? ProductionCrushed { get; set; }
        public decimal? ProductionCrushedAmount { get; set; }
    }
    public class SumProductionLedgerReport
    {

        public decimal? OrdinaryEarthAmount { get; set; }
        public decimal? OrdinaryEarth { get; set; }
        public decimal? MixedAmount { get; set; }
        public decimal? Mixed { get; set; }
        public decimal? FinedAmount { get; set; }
        public decimal? Fined { get; set; }
        public decimal? CoarseAmount { get; set; }
        public decimal? Coarse { get; set; }
        public decimal? BouldersAmount { get; set; }
        public decimal? Boulders { get; set; }
        public decimal? CrushedAmount { get; set; }
        public decimal? Crushed { get; set; }
        //production
        public decimal? ProductionOrdinaryEarth { get; set; }
        public decimal? ProductionOrdinaryEarthAmount { get; set; }
        public decimal? ProductionMixed { get; set; }
        public decimal? ProductionMixedAmount { get; set; }
        public decimal? ProductionFined { get; set; }
        public decimal? ProductionFinedAmount { get; set; }
        public decimal? ProductionCoarse { get; set; }
        public decimal? ProductionCoarseAmount { get; set; }
        public decimal? ProductionBoulders { get; set; }
        public decimal? ProductionBouldersAmount { get; set; }
        public decimal? ProductionCrushed { get; set; }
        public decimal? ProductionCrushedAmount { get; set; }
    }
}
