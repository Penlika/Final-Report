using Final_Report.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
        public ActionResult Register(ACCOUNT Model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.ACCOUNT.FirstOrDefault(k => k.EMAIL == Model.EMAIL);

                if (existingUser != null)
                {
                    ModelState.AddModelError("EMAIL", "Email already exists!");
                    return View(Model);
                }
                db.ACCOUNT.Add(Model);
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
                var u = db.ACCOUNT.FirstOrDefault(k => k.EMAIL == user.Email && k.PASSWORD == user.Password);

                if (u != null)
                {
                    Session["EMAIL"] = u;

                    // Determine user role
                    string userRole = u.ROLE;

                    if (userRole.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {
                        // Store admin information in session
                        Session["Admin"] = db.ADMIN.FirstOrDefault(admin => admin.EMAIL == u.EMAIL);

                        // Redirect to admin views
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }
                    else if (userRole.Equals("customer", StringComparison.OrdinalIgnoreCase))
                    {
                        // Redirect to customer views
                        return RedirectToAction("Index", "HomePage");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "The account does not exist or the password is incorrect.");
                }
            }

            return View(user);
        }



        public ActionResult Logout()
        {
            Session["EMAIL"] = null;
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