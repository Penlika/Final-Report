using Final_Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Data.Entity.Validation;
using System.Data.Entity;

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
                var account = db.ACCOUNT.FirstOrDefault(k => k.EMAIL == user.Email && k.PASSWORD == user.Password);

                if (account != null)
                {
                    // Store user ID in session
                    Session["UserID"] = account.ID;

                    // Determine user role
                    string userRole = account.ROLE;

                    if (userRole.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {
                        // Store admin information in session
                        Session["admin"] = account;
                        // Redirect to admin views
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }
                    else if (userRole.Equals("customer", StringComparison.OrdinalIgnoreCase))
                    {
                        // Retrieve customer data based on the email
                        CUSTOMER customer = db.CUSTOMER.FirstOrDefault(c => c.EMAIL == account.EMAIL);

                        if (customer != null)
                        {
                            // Store customer information in session
                            Session["customer"] = customer;
                            Session["EMAIL"] = account.EMAIL; // Store the email for other uses

                            // Redirect to customer views
                            return RedirectToAction("Index", "HomePage");
                        }
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
            // Retrieve customer email from session
            string userEmail = (string)Session["EMAIL"];

            // Retrieve customer data based on the email
            CUSTOMER customer = db.CUSTOMER.FirstOrDefault(c => c.EMAIL == userEmail);
            return View(customer);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(CUSTOMER model, HttpPostedFileBase fFileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    Image img = Image.FromStream(fFileUpload.InputStream, true, true);
                    model.PICTURES = Utility.ConvertImageToBase64(img);
                }
                else
                {
                    CUSTOMER existingCus = db.CUSTOMER.Find(model.USERNAME);
                    if (existingCus != null)
                    {
                        model.PICTURES = existingCus.PICTURES;
                    }
                }
                db.CUSTOMER.AddOrUpdate(model);
                db.SaveChanges();
            }
            return RedirectToAction("Profile");
        }


        public ActionResult BookingHistory()
        {
            return View();
        }
    }
}