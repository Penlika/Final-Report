using FundamentalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Final_Report.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        Model1 db=new Model1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BookingHotel(int IDHotel)
        {
            return View(IDHotel);
        }
        public ActionResult CompleteBookHotel(int IDHotel, FormCollection f)
        {
            var userLogin = (CUSTOMER)Session["USERNAME"];
            if (userLogin == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var hotel = db.HOTELs.Where(h => h.ID == IDHotel).FirstOrDefault();
                var book = new BOOKINGHOTEL
                {
                    IDHOTEL = hotel.ID,
                    IDCUSTOMER = userLogin.ID,
                    TOTALPRICE = hotel.PRICE_PER_PERSON,
                    BOOKING_DETAIL = "None",
                    NUMOFPERSON = 1,
                    STATUS = "Paid"
                };
                db.BOOKINGHOTELs.Add(book);
                db.SaveChanges();

                var mail = new SmtpClient("smtp.gmail.com", 25)
                {
                    Credentials = new NetworkCredential("hoainam3183@gmail.com", "scuc bpjv iqdk kesi"),
                    EnableSsl = true
                };

                var m = new MailMessage();
                m.From = new MailAddress("hoainam3183@gmail.com");
                m.ReplyToList.Add("hoainam3183@gmail.com");
                m.To.Add(new MailAddress(f["To"]));
                m.Subject = "Your booking has been completed !";
                m.Body = "Dear Mr/Mrs " + f["LastName"] + ",\nThis is your detail information of your booking ! \n ";

                mail.Send(m);
            }

            return RedirectToAction("Stays", "HomePage");
        }
        public ActionResult BookingPackage()
        {
            return RedirectToAction("Index", "HomePage");
        }
        public ActionResult CompleteBookPackage(int IDPackage, FormCollection f)
        {
            var userLogin = (CUSTOMER)Session["USERNAME"];
            if (userLogin == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var hotel = db.HOTELs.Where(h => h.ID == IDPackage).FirstOrDefault();
                var book = new BOOKINGHOTEL
                {
                    IDHOTEL = hotel.ID,
                    IDCUSTOMER = userLogin.ID,
                    TOTALPRICE = hotel.PRICE_PER_PERSON,
                    BOOKING_DETAIL = "None",
                    NUMOFPERSON = 1,
                    STATUS = "Paid"
                };
                db.BOOKINGHOTELs.Add(book);
                db.SaveChanges();

                var mail = new SmtpClient("smtp.gmail.com", 25)
                {
                    Credentials = new NetworkCredential("hoainam3183@gmail.com", "scuc bpjv iqdk kesi"),
                    EnableSsl = true
                };

                var m = new MailMessage();
                m.From = new MailAddress("hoainam3183@gmail.com");
                m.ReplyToList.Add("hoainam3183@gmail.com");
                m.To.Add(new MailAddress(f["To"]));
                m.Subject = "Your booking has been completed !";
                m.Body = "Dear Mr/Mrs " + f["LastName"] + ",\nThis is your detail information of your booking ! \n ";

                mail.Send(m);
            }

            return RedirectToAction("Index", "HomePage");
        }
        public ActionResult BookingFlight()
        {
            return RedirectToAction("Index", "HomePage");
        }

        public ActionResult History()
        {
            var userLogin = (CUSTOMER)Session["USERNAME"];
            List<object> model = new List<object>();
            if (userLogin == null)
            {
                return Redirect("~/User/Login");
            }
            else
            {
                var lstBHotel = db.BOOKINGHOTELs.Where(d => d.IDCUSTOMER == userLogin.ID).ToList();
                var lstBFlight = db.BOOKINGFLIGHTs.Where(d => d.IDCUSTOMER == userLogin.ID).ToList();
                var lstBPackage = db.BOOKINGPACKAGEs.Where(d => d.IDCUSTOMER == userLogin.ID).ToList();

                model.Add(lstBHotel);
                model.Add(lstBFlight);
                model.Add(lstBPackage);
            }
            return View(model);
        }
    }
}