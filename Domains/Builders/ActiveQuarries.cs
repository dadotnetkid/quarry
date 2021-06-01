using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Builders
{
    public class ActiveQuarries
    {
        public string Id { get; set; }
        public int Year { get; set; }
        public int QuarryId { get; set; }
        public int PermitteeId { get; set; }
    }
}
