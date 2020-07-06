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
    [MetadataType(typeof(QuarriesMeta))]
    public partial class Quarries
    {
        public string EntryByFullName => new UnitOfWork().UsersRepo.Find(m => m.Id == EntryBy)?.FullName;
        public string EditedByFullName => new UnitOfWork().UsersRepo.Find(m => m.Id == LastEditedBy)?.FullName;

        public List<string> BarangayIds { get; set; }
        public string BarangayName => string.Join(",", this.Barangays.Select(x => x.Barangay));

    }
    public partial class QuarriesMeta
    {
        public List<string> BarangayIds { get; set; }
        [Required(ErrorMessage = "Jurisdiction Name is required")]
        public string QuarryName { get; set; }

        public string JurisdictionName { get; set; }
        [NotMapped]
        public string EntryByFullName { get; set; }
        [NotMapped]
        public string EditedByFullName { get; set; }
    }
}
