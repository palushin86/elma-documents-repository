using DocRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using System.Web.Security;
using System.Security.Policy;

namespace DocRepository.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users model, string returnUrl)
        {
            if (ModelState.IsValid && AccountController.Login(model.Login, model.Password,
                                        persistCookie: false))
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Имя пользователя или пароль некорректны.");
            return View(model);
        }

        public static bool Login(string userName, string password, bool persistCookie = false)
        {
            bool success = false;

            using (ISession session = NHibernateHelper.OpenSession())
                {
                success = session.Query<Users>()
                    .Any(x => x.Login == userName && x.Password == password);
            }
            
            if (success)
            {
                FormsAuthentication.SetAuthCookie(userName, persistCookie);
            }
            return success;
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }

    


}