using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AU_Management.Models;
using System.Web.Security;

namespace AU_Management.Controllers
{
    public class managementController : Controller
    {
        ManagementDBContext db = new ManagementDBContext();
        //
        // GET: /management/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        #region Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register (UserMain user,String Url)
        {
            bool result = db.Users.Any(u => u.UserID == user.UserID);

            if (ModelState.IsValid && !result)
            {
                db.Users.Add(user);
                db.SaveChanges();
                FormsAuthentication.RedirectFromLoginPage(user.UserID, true);
                return RedirectToAction("Index");
            }
            else if (result)
                ModelState.AddModelError("", "帳號已存在");
            return View();
        }
        #endregion

        #region Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserMain user,String Url)
        {
            if(ModelState.IsValid)
            {
                var result = from m in db.Users
                              where m.UserID == user.UserID && m.PassWord == user.PassWord
                              select m;
                if (result.FirstOrDefault() != null)
                {
                    FormsAuthentication.RedirectFromLoginPage(user.UserID, true);
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "帳號或密碼錯誤");
            }
            return View();
        }

        #endregion

        #region Logout
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        #endregion


    }
}