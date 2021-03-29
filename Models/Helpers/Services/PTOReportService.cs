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

    public class PTOReportService : IPTOReportService
    {
        public static PTOReportService _ptoReportService;
        public readonly UnitOfWork _unitOfWork;
        private PTOReportService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public static PTOReportService Create()
        {
            if (_ptoReportService == null)
                _ptoReportService = new PTOReportService();
            return _ptoReportService;
        }

        public List<Permitees> GetPermittees(int? permitteeType)
        {
            var model = _unitOfWork.PermiteesRepo.Fetch();
            if (permitteeType > 0)
                model = model.Where(x => x.PermiteeTypeId == permitteeType);
            return model.ToList();
        }

        public List<PermiteeTypes> GetPermitteeTypes()
        {
            return _unitOfWork.PermiteeTypesRepo.Get().ToList();
        }

        public List<int> GetAvailableYear(int? permiteeId)
        {
            var model = _unitOfWork.ProductionsRepo.Fetch();
            if (permiteeId > 0)
                model = model.Where(x => x.PermiteeId == permiteeId);
            var year = model.Select(x => SqlFunctions.DatePart("year", x.ProductionDate) ?? 0).GroupBy(x => x)
                .Select(x => x.Key).ToList();
            return year;
        }

        public object GetAvailableMonth(int? permitteeId, int? year)
        {
            var model = _unitOfWork.ProductionsRepo.Fetch();
            if (permitteeId > 0)
                model = model.Where(x => x.PermiteeId == permitteeId);
            
            if (year > 0)
                model = model.Where(x => SqlFunctions.DatePart("year", x.ProductionDate) == year);

            var month = model
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
