using Final_Report.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Data.Entity.Migrations;
using System.Web.Helpers;
using Microsoft.Ajax.Utilities;

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

        [HttpGet]
        public ActionResult ForgotPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPass(FormCollection f)
        {
            var inputEmail = f["email"];
            TempData["UserMail"] = inputEmail;
            ACCOUNT user = db.ACCOUNT.FirstOrDefault(u => u.EMAIL == inputEmail);
            if (user == null)
            {
                ViewBag.Err = "This email have not register !";
            }
            else
                return RedirectToAction("ResetPass");
                //return RedirectToAction("VerifyMail", "User", new {email = inputEmail});
            return View();
        }

        [HttpGet]
        public ActionResult VerifyMail(string email)
        {
            // Generate OTP
            Random random = new Random();
            string otp = new string(Enumerable.Range(0, 6).Select(x => "0123456789"[random.Next(0, 10)]).ToArray());
            TempData["OTP"] = otp;

            var mail = new SmtpClient("smtp.gmail.com", 25)
            {
                Credentials = new NetworkCredential("hoainam3183@gmail.com", "scuc bpjv iqdk kesi"),
                EnableSsl = true
            };

            var m = new MailMessage();
            m.From = new MailAddress("hoainam3183@gmail.com");
            m.ReplyToList.Add("hoainam3183@gmail.com");
            m.To.Add(new MailAddress(email));
            m.Subject = "Verify code has been sent !";
            m.Body = "Please enter the following code to verify that you are the one who changed the password:  " + otp;

            mail.Send(m);
            ViewBag.Mail = "An email sent to your email address, please check the mailbox !";
            return View();
        }

        [HttpPost]
        public ActionResult VerifyMail(FormCollection f)
        {
            string userOtp = f["otp"];
            if (userOtp.CompareTo(TempData["OTP"]) == 0)
            {
                return RedirectToAction("ResetPass", "User");
            }
            else
            {
                ViewBag.Error = "Your OTP is incorrect!";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ResetPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPass(FormCollection f)
        {
            string tempPass = f["password"];
            if (!tempPass.IsNullOrWhiteSpace())
            {
                string email = (string)TempData["UserMail"];
                ACCOUNT user = db.ACCOUNT.SingleOrDefault(u => u.EMAIL == email);

                if (user != null)
                {
                    user.PASSWORD = tempPass;
                    db.ACCOUNT.AddOrUpdate(user);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                System.Diagnostics.Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Login");
        }

    }
}