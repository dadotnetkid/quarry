using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [MetadataType(typeof(FacilitiesMeta))]
    public partial class Facilities
    {

    }

    public class FacilitiesMeta
    {
        [Required(ErrorMessage ="Facility Name is required")]
        public string FacilityName { get; set; }
    }
}
