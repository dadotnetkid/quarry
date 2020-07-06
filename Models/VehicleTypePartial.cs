using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [MetadataType(typeof(VehicleTypeMeta))]
    public partial class VehicleTypes
    {
        public string ListVehicles { get; set; }
    }

    public class VehicleTypeMeta
    {
        [Required(ErrorMessage = "Vehicle Type Name is required")]
        public string VehicleTypeName { get; set; }
    }
}
