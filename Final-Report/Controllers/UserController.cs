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
using Microsoft.Ajax.Utilities;
using System.Net.Mail;
using System.Net;

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
                    Session["User"] = account;

                    if (userRole.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {
                        // Store admin information in session
                        Session["admin"] = db.ADMIN.FirstOrDefault(admin=>admin.EMAIL==account.EMAIL);
                        // Redirect to admin views
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }
                    if (userRole.Equals("manager", StringComparison.OrdinalIgnoreCase))
                    {
                        MANAGER man = db.MANAGER.FirstOrDefault(c => c.EMAIL == account.EMAIL);

                        Session["manager"] = man;
                        Session["EMAIL"] = man.EMAIL; // Store the email for other uses

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
                            Session["EMAIL"] = customer.EMAIL; // Store the email for other uses

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
                    CUSTOMER existingCus = db.CUSTOMER.FirstOrDefault(c => c.EMAIL == model.EMAIL);
                    if (existingCus != null)
                    {
                        model.PICTURES = existingCus.PICTURES;
                    }
                }
                db.CUSTOMER.AddOrUpdate(model);
                db.SaveChanges();
            }
            return View("Profile",model);
        }


        public ActionResult BookingHistory()
        {
            var userLogin = (CUSTOMER)Session["customer"];
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

        // Change password
        public ActionResult ChangePass()
        {
            var userLogin = (ACCOUNT)Session["User"];
            return RedirectToAction("VerifyMail2", "User", new {email = userLogin.EMAIL});
        }


        [HttpGet]
        public ActionResult VerifyMail2(string email)
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
        public ActionResult VerifyMail2(FormCollection f)
        {
            string userOtp = f["otp"];
            if (userOtp.CompareTo(TempData["OTP"]) == 0)
            {
                return RedirectToAction("ChangePass2", "User");
            }
            else
            {
                ViewBag.Error = "Your OTP is incorrect !";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ChangePass2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePass2(FormCollection f)
        {
            string tempPass = f["pass"];
            if (!tempPass.IsNullOrWhiteSpace())
            {
                var email = (string)Session["EMAIL"];
                ACCOUNT user = db.ACCOUNT.SingleOrDefault(u => u.EMAIL == email);

                if (user != null)
                {
                    user.PASSWORD = tempPass;
                    user.CONFIRMPASSWORD = tempPass;
                    db.ACCOUNT.AddOrUpdate(user);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Login");
        }

        // Reset password
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
                return RedirectToAction("VerifyMail", "User", new { email = inputEmail });
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
                    user.CONFIRMPASSWORD = tempPass;
                    db.ACCOUNT.AddOrUpdate(user);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Login");
        }
    }
}