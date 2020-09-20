using B_KOM_Sklep_internetowy.App_Start;
using B_KOM_Sklep_internetowy.DAL;
using B_KOM_Sklep_internetowy.Models;
using B_KOM_Sklep_internetowy.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace B_KOM_Sklep_internetowy.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        InternetShopContext db = new InternetShopContext();
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        // GET: Manage
        public async Task<ActionResult> AccountSettings(ManageMessageId? message)
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserAdmin = true;
            else
                ViewBag.UserAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }

            var model = new ManageCredentialViewModel
            {
                Message = message,
                UserData = user.UserData
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "UserData")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.UserData = userData;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }
            else
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("AccountSettings");
            }
            return RedirectToAction("AccountSettings");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("AccountSettings");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("AccountSettings", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("AccountSettings");
            }

            var message = ManageMessageId.ChangePasswordSuccess;
            return RedirectToAction("AccountSettings", new { Message = message });
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }


        public ActionResult OrdersList()
        {
            IEnumerable<Order> userOrders;

            var userId = User.Identity.GetUserId();
            userOrders = db.Orders.Where(c => c.UserId == userId).Include("OrderItems").OrderByDescending(c => c.OrderDate).ToList();

            return View(userOrders);
        }

        public ActionResult OrderDetails(int id)
        {
            Order userOrderById;

            var userId = User.Identity.GetUserId();
            userOrderById = db.Orders.Where(c => c.UserId == userId && c.OrderId == id).Include("OrderItems").FirstOrDefault();

            if (userOrderById == null)
            {
                return RedirectToAction("Error", "Common");
            }

            return View(userOrderById);
        }
    }
}