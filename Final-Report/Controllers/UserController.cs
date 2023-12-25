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
                        Session["admin"] = db.ADMIN.FirstOrDefault(admin => admin.EMAIL == u.EMAIL);

                        // Redirect to admin views
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }
                    else if (userRole.Equals("customer", StringComparison.OrdinalIgnoreCase))
                    {
                        Session["customer"] = u;
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
        [HttpGet]
        public ActionResult Profile()
        {
            string userEmail = ((ACCOUNT)Session["customer"]).EMAIL;

            // Retrieve customer data based on the email
            CUSTOMER customer = db.CUSTOMER.FirstOrDefault(c => c.EMAIL == userEmail);
            return View(customer);
        }
        [HttpPost]
        public ActionResult UpdateProfile(CUSTOMER updatedCustomer)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the original customer from the database
                CUSTOMER originalCustomer = db.CUSTOMER.FirstOrDefault(c => c.EMAIL == updatedCustomer.EMAIL);

                if (originalCustomer != null)
                {
                    // Update the properties of the original customer with the updated values
                    originalCustomer.NAME = updatedCustomer.NAME;
                    originalCustomer.PHONENUMBER = updatedCustomer.PHONENUMBER;
                    originalCustomer.ADDRESS = updatedCustomer.ADDRESS;

                    // Save changes to the database
                    db.SaveChanges();

                    // Redirect to the profile page after successful update
                    return RedirectToAction("Profile");
                }
            }

            // If the model state is not valid or an issue occurred during update, return to the profile page with the current data
            return View("Profile", updatedCustomer);
        }

        public ActionResult BookingHistory()
        {
            var userLogin = (ACCOUNT)Session["EMAIL"];
            List<object> model = new List<object>();
            if (userLogin == null)
            {
                return Redirect("~/User/Login");
            }
            else
            {
                var lstBHotel = db.BOOKINGHOTEL.Where(d => d.IDCUSTOMER == userLogin.ID).ToList();
                var lstBFlight = db.BOOKINGFLIGHT.Where(d => d.IDCUSTOMER == userLogin.ID).ToList();

                model.Add(lstBHotel);
                model.Add(lstBFlight);
            }
            return View(model);
        }
    }
}