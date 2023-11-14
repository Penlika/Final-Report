using Final_Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Final_Report.Controllers
{
    public class UserController : Controller
    {
        Final db = new Final();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(USER Model)
        {
            if (ModelState.IsValid)
            {
                var tk = db.USERS.FirstOrDefault(k => k.USERNAME == Model.USERNAME);
                if (tk != null)
                {
                    ModelState.AddModelError("USERNAME", "This account is already exist");
                    return View(Model);
                }
                db.USERS.Add(Model);
                db.SaveChanges();
                return RedirectToAction("Login", "User");
            }
            return View(Model);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                var u = db.USERS.FirstOrDefault(k => k.USERNAME == user.UserName && k.PASSWORD == user.Password);
                if (u != null)
                {
                    Session["USERNAME"] = u;
                }
                else
                {
                    ModelState.AddModelError("PASSWORD", "This account is not exist");
                }
            }
            return RedirectToAction("index", "HomePage");
        }
        public ActionResult Logout()
        {
            Session["USERNAME"] = null;
            return Redirect("~/");
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult BookingHistory()
        {
            return View();
        }
    }
}