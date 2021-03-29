﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class BIRParamsViewModel
    {
     
        public int QuarriesId { get; set; }
        public int PermitteeType { get; set; }
        public int Permittee { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
