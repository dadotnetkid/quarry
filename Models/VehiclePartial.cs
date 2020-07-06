using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models
{
    [MetadataType(typeof(VehicleMeta))]
    public partial class Vehicles
    {
        public string VehicleName => this.VehicleTypes.VehicleTypeName + " " + this.PlateNo;
        public bool IsNew { get; set; }
    }

    public class VehicleMeta
    {
        [Required(ErrorMessage = "Permitee is required")]
        public int PermiteeId { get; set; }
        [Required(ErrorMessage = "Vehicle Type is required")]
        public int VehicleTypeId { get; set; }
        [Required(ErrorMessage = "Plate No is required")]
        public string PlateNo { get; set; }
    }

}
