using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [MetadataType(typeof(PermitteeTypesMeta))]
    public partial class PermitteeTypes
    {

    }

    public class PermitteeTypesMeta
    {
        [Required(ErrorMessage = "Permitee Type Name is required")]
        public string PermiteeTypeName { get; set; }
    }
}
