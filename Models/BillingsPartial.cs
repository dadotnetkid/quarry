using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ViewModels;

namespace Models
{
    public partial class Billings
    {
        private string _officialReceipt;

        public string OfficialReceipt
        {
            get
            {
                if (_officialReceipt == null)
                    _officialReceipt = Transactions?.OfficialReceipt;
                return _officialReceipt;
            }
            set => _officialReceipt = value;
        }

        public string AmountInWord { get; set; }
        public List<BillingStatementViewModel> BillingStatementViewModels { get; set; }
    }
}
