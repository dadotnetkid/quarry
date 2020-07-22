using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class AccountingReportViewModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public Quarries Quarries { get; set; }
        public int QuarryId { get; set; }
        public string OfficialReceipt { get; set; }
        public decimal Amount { get; set; }
        public decimal Extraction { get; set; }
        public decimal Barangay => this.Extraction * 0.4M;
        public decimal Municipality => this.Extraction * 0.4M;
        public decimal Provicial => this.Extraction * 0.3M;
        public Permitees Permittee { get; set; }
        public int PermitteeId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
