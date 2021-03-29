using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Repository;

namespace Helpers.Services
{
    public class BIRReportService
    {
        private static BIRReportService bIRReport;
        public static BIRReportService Create()
        {
            if (bIRReport == null)
                bIRReport = new BIRReportService();
            return bIRReport;
        }

        private ModelDb db = new ModelDb();
        private readonly UnitOfWork _unitOfWork;

        private BIRReportService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<PermiteeTypes> GetAvailablePermitteeType(int? quarryId)
        {
            var model = _unitOfWork.PermiteeTypesRepo.Fetch();
            if (quarryId != null)
                model = model.Where(x => x.Permitees.Any(m => m.Quarries.Any(p => p.Id == quarryId)));
            return model.ToList();
        }
        public List<Permitees> GetAvailablePermittee(int? permitteeTypeId)
        {
            var model = _unitOfWork.PermiteesRepo.Fetch();
            if (permitteeTypeId != null)
                model = model.Where(x => x.PermiteeTypeId == permitteeTypeId);
            return model.ToList();
        }

        public List<Quarries> GetQuarries()
        {
            return _unitOfWork.QuarriesRepo.Get().ToList();
        }
        public List<int> GetAvailableYear(int? permitteeId)
        {

            var permittee = _unitOfWork.ProductionsRepo.Fetch();
            if (permitteeId > 0)
            {
                permittee = permittee.Where(x => x.PermiteeId == permitteeId);
            }

            var year = permittee
                .GroupBy(x => SqlFunctions.DatePart("year", x.DateCreated) ?? 0)
                .Where(x => x.Key != 0)
                .Select(x => x.Key).ToList();
            return year;
        }
        public object GetAvailableMonths(int? year, int? permitteeId,int? quarryId)
        {
            var permittee = _unitOfWork.ProductionsRepo.Fetch();
            if (permitteeId > 0 )
            {
                permittee = permittee.Where(x => x.PermiteeId == permitteeId);
            }

            if (permitteeId > 0)
            {
                permittee = permittee.Where(x => SqlFunctions.DatePart("year", x.DateCreated) == year);
            }

            if (quarryId > 0)
            {
                permittee = permittee.Where(x => x.QuarriesId == quarryId);
            }


            var month = permittee
                .GroupBy(x => SqlFunctions.DatePart("month", x.DateCreated) ?? 0)
                .Where(x => x.Key != 0)
                .Select(x => x.Key).ToList();

            return month.Select(x => new
            {
                MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x),
                Month = x

            }).ToList();

        }
    }
}
