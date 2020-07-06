using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Repository;

namespace Helpers
{
    public class PermiteeHelper
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public string AccreditationNumber => accreditationNumber();
        private string accreditationNumber()
        {
            var permitees =
                unitOfWork.PermiteesRepo.Get().OrderByDescending(m => Convert.ToInt32(m.AccreditationNumber.Split('-')[1])).FirstOrDefault();
            if (permitees?.AccreditationNumber != null)
            {
                var accreditationNumber = Convert.ToInt32(permitees?.AccreditationNumber?.Split('-')?[1]) + 1;
                return DateTime.Now.Year + "-" + accreditationNumber.ToString("d5");
            }

            return DateTime.Now.Year + "-" + "00001";

        }
    }
}
