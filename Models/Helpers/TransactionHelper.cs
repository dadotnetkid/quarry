using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Repository;

namespace Helpers
{
    public class TransactionHelper
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public string TransactionNumber => transactionNumber();
        private string transactionNumber()
        {
            var item =
                unitOfWork.TransactionsRepo.Get().OrderByDescending(m => Convert.ToInt32(m.TransactionNumber.Split('-')[1])).FirstOrDefault();
            if (item?.TransactionNumber != null)
            {
                var accreditationNumber = Convert.ToInt32(item?.TransactionNumber?.Split('-')?[1]) + 1;
                return DateTime.Now.Year + "-" + accreditationNumber.ToString("d5");
            }

            return DateTime.Now.Year + "-" + "00001";

        }
    }
}
