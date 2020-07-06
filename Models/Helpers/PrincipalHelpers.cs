using System.Linq;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Models.Repository;

namespace Helpers
{
    public static class PrincipalHelpers
    {

        public static bool IsInRoles(this IPrincipal principal, params string[] roles)
        {
            Models.Repository.UnitOfWork unitOfWork = new Models.Repository.UnitOfWork();
            var userId = principal.Identity.GetUserId();
            var user = unitOfWork.UsersRepo.Find(m => m.Id == userId);
            if (principal == null)
                return false;
            if (user == null)
                return false;
            if (string.IsNullOrEmpty(userId))
                return false;
            return user.UserRoles.Any(m => roles.Contains(m.Name));
        }

        public static bool UserIsInRoles(string userId, params string[] roles)
        {
            var unitOfWork = new UnitOfWork();

            var user = unitOfWork.UsersRepo.Find(m => m.Id == userId);

            if (user == null)
                return false;
            if (string.IsNullOrEmpty(userId))
                return false;
            return user.UserRoles.Any(m => roles.Contains(m.Name));
        }
        public static bool UserIsInAction(this IPrincipal principal, params string[] actionName)
        {
            /*var unitOfWork = new UnitOfWork();
            var userId = principal.Identity.GetUserId();
            var users = unitOfWork.UsersRepo.Fetch(m => m.Id == userId).Any(m => m.UserRoles.Any(x => x.UserRolesInActions.Any(u => actionName.Contains(u.Action))));
            return users;*/
            var unitOfWork = new UnitOfWork();
            var userId = principal.Identity.GetUserId();
            var x = unitOfWork.UsersRepo.Find(m => m.Id == userId);
            if (!unitOfWork.ActionsRepo.Fetch(m => actionName.Contains(m.Action)).Any())
            {
                foreach (var i in actionName)
                {
                    unitOfWork.ActionsRepo.Insert(new Models.Actions() { Action = i });
                    unitOfWork.Save();
                }

            }
            var role = unitOfWork.UserRolesRepo.Find(m => m.Name == "Administrator");
            if (role != null)
                if (role.UserRolesInActions.All(m => !actionName.Contains(m.Action)))
                {
                    foreach (var i in actionName)
                    {
                        role.UserRolesInActions.Add(new Models.UserRolesInActions() { Action = i });
                        unitOfWork.Save();
                    }

                }

            if (x.UserRoles.Any(c => c.UserRolesInActions.Any(n => actionName.Contains(n.Action))))
                return true;

            return false;
        }
        public static bool UserInAction(this IPrincipal principal, string action)
        {
            var unitOfWork = new UnitOfWork();
            var userId = principal.Identity.GetUserId();
            var x = unitOfWork.UsersRepo.Find(m => m.Id == userId);
            if (!unitOfWork.ActionsRepo.Fetch(m => m.Action == action).Any())
            {
                unitOfWork.ActionsRepo.Insert(new Models.Actions() { Action = action });
                unitOfWork.Save();
            }
            var role = unitOfWork.UserRolesRepo.Find(m => m.Name == "Administrator");
            if (role != null)
                if (role.UserRolesInActions.All(m => m.Action != action))
                {
                    role.UserRolesInActions.Add(new Models.UserRolesInActions() { Action = action });
                    unitOfWork.Save();
                }

            if (x.UserRoles.Any(c => c.UserRolesInActions.Any(n => n.Action == action)))
                return true;

            return false;
        }
    }
}
