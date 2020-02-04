using CookieAuth.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CookieAuth.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AccountViewModel avm, string returnUrl)
        {
            if (string.IsNullOrEmpty(avm.Account.Username) || string.IsNullOrEmpty(avm.Account.Password))
            {
                ViewBag.Error = "Account's Invalid";
                return View("Index");
            } else
            {
                if (IsValid(avm))
                {
                    FormsAuthentication.SetAuthCookie(avm.Account.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                return View(avm);
            }
        }

        private bool IsValid(AccountViewModel avm)
        {
            return (avm.Account.Username.Equals("test") && avm.Account.Password.Equals("test"));
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }
    }
}