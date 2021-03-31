using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Repository;

namespace Helpers.Services
{
    public class BillingMasterListReportService : IBillingMasterListReportService
    {
        private static BillingMasterListReportService _billingMasterListReportService;
        private readonly UnitOfWork _unitOfWork;

        private BillingMasterListReportService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public static BillingMasterListReportService Create()
        {
            return _billingMasterListReportService ??
                   (_billingMasterListReportService = new BillingMasterListReportService());
        }

        public List<PermiteeTypes> GetPermitteeType()
        {
            return new List<PermiteeTypes>();
        }
    }
}
