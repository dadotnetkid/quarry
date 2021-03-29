using System.Collections.Generic;
using Models;

namespace Helpers.Services
{
    public interface IPTOReportService
    {
        List<Permitees> GetPermittees(int? permitteeType);
        List<PermiteeTypes> GetPermitteeTypes();
        List<int> GetAvailableYear(int? permiteeId);
        object GetAvailableMonth(int? permitteeId,int? year);
    }
}