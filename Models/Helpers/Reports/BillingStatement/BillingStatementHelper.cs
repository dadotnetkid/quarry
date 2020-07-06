using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Repository;

namespace Helpers.Reports.BillingStatement
{
    public class BillingStatementHelper
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public rptBillingStatement report(string transactionId)
        {
            rptBillingStatement rpt = new rptBillingStatement()
            {
                DataSource=unitOfWork.TransactionsRepo.Get(m=>m.Id==transactionId)
            };
            return rpt;
        }
    }
}
