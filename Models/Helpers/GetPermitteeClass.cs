using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Helpers
{
    public static class GetPermiteeClass
    {
        public static string PermiteeeClass(this Permitees Permitees, PermiteeMode mode)
        {
            if (Permitees == null)
                return "bg-aqua";

            if (!Permitees.Transactions.Any() && mode==PermiteeMode.Register)
            {
                return "bg-aqua";
            }
            if (Permitees.Transactions.Any() && mode == PermiteeMode.Renew)
            {
                return "bg-aqua";
            }

            return "";
        }


        public static string GetRegisterClass(this Permitees item)
        {
            if(item==null)
                return "bg-aqua";
            if (item.Transactions.Any())
                return "bg-gray disable-link";
            return "bg-aqua";
        }

        public static string GetHasTransactionClass(this Permitees item)
        {
            if (item == null)
                return "bg-gray disable-link";
            if (item.Transactions.Any())
                return "bg-aqua";
            return "bg-gray disable-link";
        }
    }

    public enum PermiteeMode
    {
        Register=0,
        Renew=1,
        Add=2,
        AdvPayment=3,
        View=4
    }
}
