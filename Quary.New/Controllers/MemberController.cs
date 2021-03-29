﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Models;
using Models.ControllerHelpers;
using Models.Repository;
using Models.ViewModels;

namespace Quary.New.Controllers
{
    public class MemberController : IdentityController
    {
        // GET: Member
        private UnitOfWork unitOfWork = new UnitOfWork();

        #region Forgot Password
        [AllowAnonymous, Route("change-password")]
        public ActionResult ChangeForgotPasswordViewPartial(string Email, string Token)
        {
            ViewBag.Email = Email;
            ViewBag.Token = Token;
            return View();
        }
        [HttpPost, AllowAnonymous, Route("change-password")]
        public async Task<ActionResult> ChangeForgotPasswordViewPartial(ForgotPassword forgotPassword)
        {
            var user = await UserManager.FindByEmailAsync(forgotPassword.EmailAddress);
            var res = await UserManager.ResetPasswordAsync(user.Id, forgotPassword.Token, forgotPassword.NewPassword);
            return PartialView("ChangeForgotPasswordViewPartial");
        }
        public async Task<PartialViewResult> ForgotPopupControlPartial([ModelBinder(typeof(DevExpressEditorsBinder))]string emailAddress)
        {
            if (emailAddress != null)
            {
                try
                {
                    var user = unitOfWork.UsersRepo.Find(m => m.Email == emailAddress);
                    var token = UserManager.GeneratePasswordResetToken(user?.Id);
                    //var Token = await UserManager.GeneratePasswordResetTokenAsync(user?.Id);
                    var confirmationlink = $"<h3>Forgot Password</h3><br/><a href='{Url.Action("ChangeForgotPasswordViewPartial", "Member", new { Email = user.Email, Token = token }, Request.Url.Scheme)}'>Click here to change your Password</a>";
                    await UserManager.SendEmailAsync(userId: user.Id, subject: "Forgot Password", body: confirmationlink);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return PartialView();
        }


        #endregion
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public async Task<ActionResult> Login([ModelBinder(typeof(DevExpressEditorsBinder))]LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var user = UserManager.FindByEmail(model.UserName);
            if (unitOfWork.UsersRepo.Fetch(x => x.UserName == model.UserName).Any())
            {
                user = UserManager.FindByName(model.UserName);
            }
             
            if (user == null)
            {
                ModelState.AddModelError("UserName", "Username not exist");
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(user?.UserName, model.Password, model.RememberMe ?? false, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("UserName", "Invalid Email.");
                    ModelState.AddModelError("Password", "Invalid Password.");
                    return View(model);
            }
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var user = new Users() { Id = Guid.NewGuid().ToString(), UserName = model.Email, Email = model.Email, FirstName = model.FirstName, MiddleName = model.MiddleName, LastName = model.LastName };
            var res = await UserManager.CreateAsync(user, model.Password);
            return RedirectToAction("Login");
        }

        public ActionResult RegisterPartial(RegistrationType registrationType)
        {
            return PartialView(new RegisterViewModel() { RegistrationType = registrationType });
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Transactions");
        }
        [Route("log-off")]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Transactions");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        public ActionResult Users()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult UserGridViewPartial()
        {
            var model = new object[0];
            return PartialView("_UserGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UserGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Users item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_UserGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UserGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Users item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_UserGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UserGridViewPartialDelete(System.String Id)
        {
            var model = new object[0];
            if (Id != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_UserGridViewPartial", model);
        }
    }
}