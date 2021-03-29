using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class PTOReportParams
    {
        public int PermitteeId  { get; set; }
        public int PermitteeTypeId { get; set; }
        public int Year { get; set; }
    }
}
