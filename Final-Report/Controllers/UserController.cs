using Final_Report.Models;
using FundamentalProject.Models;
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
        Model1 db = new Model1();
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
        public ActionResult Register(CUSTOMER Model)
        {
            if (ModelState.IsValid)
            {
                var cus = db.CUSTOMERs.FirstOrDefault(k => k.USERNAME == Model.USERNAME);
                if (cus != null)
                {
                    ModelState.AddModelError("USERNAME", "Username already existed !");
                    return View(Model);
                }
                db.CUSTOMERs.Add(Model);
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
                var account = (from u in db.USERS
                               join cus in db.CUSTOMERs
                               on u.ID equals cus.ID
                               select new { USERNAME = cus.USERNAME, PASSWORD = u.PASSWORD });
                var c = account.FirstOrDefault(k => k.USERNAME == user.UserName && k.PASSWORD == user.Password);
                if (c != null)
                {
                    Session["USERNAME"] = c;
                    return RedirectToAction("Index", "HomePage");
                }
                else
                    ModelState.AddModelError("Password", "Password incorrect !");
            }
            return RedirectToAction("Index", "HomePage");
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