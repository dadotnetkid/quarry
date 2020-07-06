using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Repository;

namespace Quary.New.Controllers
{
    public class Validations
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Validations
        public bool IsPlateAvailable(string PlateNo)
        {
          
            return unitOfWork.VehiclesRepo.Fetch(m => m.PlateNo == PlateNo).Any();
        }
    }
}