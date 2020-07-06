using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class SummaryProgramOfWorks
    {
        public decimal? Amount => this.Sags?.UnitCost * this.Quantity;

        public decimal? OrdinaryEarth => SagId == 1 ? Quantity : 0;
        public decimal? OrdinaryAmount => this.ProgramOfWorks?.Permitees.PermiteeTypes?.PermiteeTypeName == "Government Gratuitous" ? 0 : SagId == 1 ? this.Sags?.UnitCost * Quantity : 0.0M;

        public decimal? Mixed => SagId == 4 ? Quantity : 0;
        public decimal? MixedAmount => this.ProgramOfWorks?.Permitees.PermiteeTypes?.PermiteeTypeName == "Government Gratuitous" ? 0 : SagId == 4 ? this.Sags?.UnitCost * Quantity : 0.0M;
        public decimal? FineScreen => SagId == 5 ? Quantity : 0;
        public decimal? FineScreenAmount => this.ProgramOfWorks?.Permitees.PermiteeTypes?.PermiteeTypeName == "Government Gratuitous" ? 0 : SagId == 5 ? this.Sags?.UnitCost * Quantity : 0.0M;
        public decimal? Coarse => SagId == 8 ? Quantity : 0;
        public decimal? CoarseAmount => this.ProgramOfWorks?.Permitees.PermiteeTypes?.PermiteeTypeName == "Government Gratuitous" ? 0 : SagId == 8 ? this.Sags?.UnitCost * Quantity : 0.0M;
        public decimal? Boulders => SagId == 6 ? Quantity : 0;
        public decimal? BouldersAmount => this.ProgramOfWorks?.Permitees.PermiteeTypes?.PermiteeTypeName == "Government Gratuitous" ? 0 : SagId == 6 ? this.Sags?.UnitCost * Quantity : 0.0M;
        public decimal? CrushedRocks => SagId == 7 ? Quantity : 0;
        public decimal? CrushedRocksAmount => this.ProgramOfWorks?.Permitees.PermiteeTypes?.PermiteeTypeName == "Government Gratuitous" ? 0 : SagId == 7 ? this.Sags?.UnitCost * Quantity : 0.0M;

        public decimal? Total => OrdinaryEarth + Mixed + FineScreen + Coarse + Boulders + CrushedRocks;
        public decimal? TotalAmount => this.ProgramOfWorks?.Permitees.PermiteeTypes?.PermiteeTypeName == "Government Gratuitous" ? 0 : OrdinaryAmount + MixedAmount + FineScreenAmount + CoarseAmount + BouldersAmount + CrushedRocksAmount;

    }

    public class PowVsHauled : ReportSummaryProgramOfWorks
    {

    }
    public class PowVsApplied : ReportSummaryProgramOfWorks
    {

    }
    public class AppliedVsHauled : ReportSummaryProgramOfWorks
    {

    }

    public class ReportSummaryProgramOfWorks : IReportSummaryProgramOfWorks
    {
        public decimal? Amount { get; set; }
        public decimal? OrdinaryEarth { get; set; }
        public decimal? OrdinaryAmount { get; set; }
        public decimal? Mixed { get; set; }
        public decimal? MixedAmount { get; set; }
        public decimal? FineScreen { get; set; }
        public decimal? FineScreenAmount { get; set; }
        public decimal? Coarse { get; set; }
        public decimal? CoarseAmount { get; set; }
        public decimal? Boulders { get; set; }
        public decimal? BouldersAmount { get; set; }
        public decimal? CrushedRocks { get; set; }
        public decimal? CrushedRocksAmount { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? ParticularId { get; set; }
        public Particulars Particulars { get; set; }
        public Permitees Permittee { get; set; }
    }
    public interface IReportSummaryProgramOfWorks
    {

        decimal? Amount { get; set; }

        decimal? OrdinaryEarth { get; set; }
        decimal? OrdinaryAmount { get; set; }

        decimal? Mixed { get; set; }
        decimal? MixedAmount { get; set; }
        decimal? FineScreen { get; set; }
        decimal? FineScreenAmount { get; set; }
        decimal? Coarse { get; set; }
        decimal? CoarseAmount { get; set; }
        decimal? Boulders { get; set; }
        decimal? BouldersAmount { get; set; }
        decimal? CrushedRocks { get; set; }
        decimal? CrushedRocksAmount { get; set; }

        decimal? Total { get; set; }
        decimal? TotalAmount { get; set; }
        int? ParticularId { get; set; }
        Particulars Particulars { get; set; }
        Permitees Permittee { get; set; }
    }
}
