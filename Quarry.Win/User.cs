using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helpers;
using Models;
using Models.Repository;

namespace Quarry.Win
{
    public static class User
    {
        public static string UserName => GetUserName();
        public static string UserId;

        public static string GetUserName()
        {
            var res = new UnitOfWork().UsersRepo.Find(x => x.Id == UserId)
                ?.UserName;
            return res;
        }

        public static string GetFullName()
        {
            var fullName = new UnitOfWork().UsersRepo.Fetch(m => m.Id == UserId).FirstOrDefault()?.FullName;
            return fullName;
        }
        public static string GetFullName(string userId)
        {
            var fullName = new UnitOfWork().UsersRepo.Fetch(m => m.Id == userId).FirstOrDefault()?.FullName;
            return fullName;
        }
        public static string GetUserLevel()
        {
            return new UnitOfWork().UsersRepo.Fetch(m => m.Id == UserId).FirstOrDefault()?.UserRole;
        }
       
    }
}
