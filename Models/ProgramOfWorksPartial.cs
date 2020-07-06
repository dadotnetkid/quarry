using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class ProgramOfWorks
    {
        public List<ReportSummaryProgramOfWorks> ReportSummaryProgramOfWorks { get; set; }

        public IReportSummaryProgramOfWorks AppliedVsHauleds => new AppliedVsHauled()
        {
            OrdinaryEarth = -this.Productions.Sum(m => m.OrdinaryEarth) + Hauleds.Sum(m => m.OrdinaryEarth),
            OrdinaryAmount = -this.Productions.Sum(m => m.OrdinaryAmount) + Hauleds.Sum(m => m.OrdinaryAmount),
            Mixed = -this.Productions.Sum(m => m.Mixed) + Hauleds.Sum(m => m.Mixed),
            MixedAmount = -this.Productions.Sum(m => m.MixedAmount) + Hauleds.Sum(m => m.MixedAmount),
            FineScreen = -this.Productions.Sum(m => m.FineScreen) + Hauleds.Sum(m => m.FineScreen),
            FineScreenAmount = -this.Productions.Sum(m => m.FineScreenAmount) + Hauleds.Sum(m => m.FineScreenAmount),
            Coarse = -this.Productions.Sum(m => m.Coarse) + Hauleds.Sum(m => m.Coarse),
            CoarseAmount = -this.Productions.Sum(m => m.CoarseAmount) + Hauleds.Sum(m => m.CoarseAmount),
            Boulders = -this.Productions.Sum(m => m.Boulders) + Hauleds.Sum(m => m.Boulders),
            BouldersAmount = -this.Productions.Sum(m => m.BouldersAmount) + Hauleds.Sum(m => m.BouldersAmount),
            CrushedRocks = -this.Productions.Sum(m => m.CrushedRocks) + Hauleds.Sum(m => m.CrushedRocks),
            CrushedRocksAmount = -this.Productions.Sum(m => m.CrushedRocksAmount) + Hauleds.Sum(m => m.CrushedRocksAmount),
            Total = -this.Productions.Sum(m => m.Total) + Hauleds.Sum(m => m.Total),
            TotalAmount = -this.Productions.Sum(m => m.TotalAmount) + Hauleds.Sum(m => m.TotalAmount),
        };

        public IReportSummaryProgramOfWorks PowVsApplied => new PowVsApplied()
        {
            OrdinaryEarth = -this.ReportSummaryProgramOfWorks.Sum(m => m.OrdinaryEarth) + Productions.Sum(m => m.OrdinaryEarth),
            OrdinaryAmount = -this.ReportSummaryProgramOfWorks.Sum(m => m.OrdinaryAmount) + Productions.Sum(m => m.OrdinaryAmount),
            Mixed = -this.ReportSummaryProgramOfWorks.Sum(m => m.Mixed) + Productions.Sum(m => m.Mixed),
            MixedAmount = -this.ReportSummaryProgramOfWorks.Sum(m => m.MixedAmount) + Productions.Sum(m => m.MixedAmount),
            FineScreen = -this.ReportSummaryProgramOfWorks.Sum(m => m.FineScreen) + Productions.Sum(m => m.FineScreen),
            FineScreenAmount = -this.ReportSummaryProgramOfWorks.Sum(m => m.FineScreenAmount) + Productions.Sum(m => m.FineScreenAmount),
            Coarse = -this.ReportSummaryProgramOfWorks.Sum(m => m.Coarse) + Productions.Sum(m => m.Coarse),
            CoarseAmount = -this.ReportSummaryProgramOfWorks.Sum(m => m.CoarseAmount) + Productions.Sum(m => m.CoarseAmount),
            Boulders = -this.ReportSummaryProgramOfWorks.Sum(m => m.Boulders) + Productions.Sum(m => m.Boulders),
            BouldersAmount = -this.ReportSummaryProgramOfWorks.Sum(m => m.BouldersAmount) + Productions.Sum(m => m.BouldersAmount),
            CrushedRocks = -this.ReportSummaryProgramOfWorks.Sum(m => m.CrushedRocks) + Productions.Sum(m => m.CrushedRocks),
            CrushedRocksAmount = -this.ReportSummaryProgramOfWorks.Sum(m => m.CrushedRocksAmount) + Productions.Sum(m => m.CrushedRocksAmount),
            Total = -this.ReportSummaryProgramOfWorks.Sum(m => m.Total) + Productions.Sum(m => m.Total),
            TotalAmount = -this.ReportSummaryProgramOfWorks.Sum(m => m.TotalAmount) + Productions.Sum(m => m.TotalAmount),
        };
        public IReportSummaryProgramOfWorks PowVsHauled => new PowVsHauled()
        {
            OrdinaryEarth = this.ReportSummaryProgramOfWorks.Sum(m => m.OrdinaryEarth) - Hauleds.Sum(m => m.OrdinaryEarth),
            OrdinaryAmount = this.ReportSummaryProgramOfWorks.Sum(m => m.OrdinaryAmount) - Hauleds.Sum(m => m.OrdinaryAmount),
            Mixed = this.ReportSummaryProgramOfWorks.Sum(m => m.Mixed) - Hauleds.Sum(m => m.Mixed),
            MixedAmount = this.ReportSummaryProgramOfWorks.Sum(m => m.MixedAmount) - Hauleds.Sum(m => m.MixedAmount),
            FineScreen = this.ReportSummaryProgramOfWorks.Sum(m => m.FineScreen) - Hauleds.Sum(m => m.FineScreen),
            FineScreenAmount = this.ReportSummaryProgramOfWorks.Sum(m => m.FineScreenAmount) - Hauleds.Sum(m => m.FineScreenAmount),
            Coarse = this.ReportSummaryProgramOfWorks.Sum(m => m.Coarse)- Hauleds.Sum(m => m.Coarse),
            CoarseAmount = this.ReportSummaryProgramOfWorks.Sum(m => m.CoarseAmount) - Hauleds.Sum(m => m.CoarseAmount),
            Boulders = this.ReportSummaryProgramOfWorks.Sum(m => m.Boulders) - Hauleds.Sum(m => m.Boulders),
            BouldersAmount = this.ReportSummaryProgramOfWorks.Sum(m => m.BouldersAmount) - Hauleds.Sum(m => m.BouldersAmount),
            CrushedRocks = this.ReportSummaryProgramOfWorks.Sum(m => m.CrushedRocks) - Hauleds.Sum(m => m.CrushedRocks),
            CrushedRocksAmount = this.ReportSummaryProgramOfWorks.Sum(m => m.CrushedRocksAmount) - Hauleds.Sum(m => m.CrushedRocksAmount),
            Total = this.ReportSummaryProgramOfWorks.Sum(m => m.Total) - Hauleds.Sum(m => m.Total),
            TotalAmount = this.ReportSummaryProgramOfWorks.Sum(m => m.TotalAmount) - Hauleds.Sum(m => m.TotalAmount),
        };
    }
}
