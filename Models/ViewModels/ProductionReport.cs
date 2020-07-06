using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ProductionReport
    {
        public Permitees Permittees { get; set; }
        public string ProjectName { get; set; }
        public int DrNo { get; set; }
        public Vehicles Vehicles { get; set; }
        public decimal? OrdinaryEarth { get; set; }
        public decimal? Mixed { get; set; }
        public decimal? ScreenFine { get; set; }
        public decimal? ScreenCoarse { get; set; }
        public decimal? Boulders { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Crushed { get; set; }
    }
}
