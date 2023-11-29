using Final_Report.Models;
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
        Model1 db = new Model1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BookingHotel(int IDHotel)
        {
            //var userLogin = (CUSTOMER)Session["USERNAME"];
            //if (userLogin == null)
            //{
            //    return RedirectToAction("Login", "User");
            //}
            //else
            //{
            //    var hotel = db.HOTELs.Where(h => h.ID == IDHotel).FirstOrDefault();
            //    return View(hotel);
            //}
            var hotel = db.HOTEL.Where(h => h.ID == IDHotel).FirstOrDefault();
            return View(hotel);
        }

        public double calTotalPriceHotel(double price, int room, DateTime checkIn, DateTime checkOut)
        {
            double totalPrice;
            int days = (checkOut - checkIn).Days;
            totalPrice = price * days * room;
            return totalPrice;
        }

        public ActionResult CompleteBookHotel(FormCollection f)
        {
            var IDHotel = Int32.Parse(f["ID"]);
            var hotel = db.HOTEL.Where(h => h.ID == IDHotel).FirstOrDefault();
            double totalPrice = calTotalPriceHotel(hotel.PRICE_PER_PERSON, Int32.Parse(f["room"]), DateTime.Parse(f["checkIn"]), DateTime.Parse(f["checkOut"]));
            var book = new BOOKINGHOTEL
            {
                IDHOTEL = hotel.ID,
                IDCUSTOMER = 6,
                CHECKINDATE = DateTime.Parse(f["checkIn"]),
                CHECKOUTDATE = DateTime.Parse(f["checkOut"]),
                NUMOFPERSON = Int32.Parse(f["guest"]),
                ROOM = Int32.Parse(f["room"]),
                TOTALPRICE = totalPrice
            };
            db.BOOKINGHOTEL.Add(book);
            db.SaveChanges();

            //var mail = new SmtpClient("smtp.gmail.com", 25)
            //{
            //    Credentials = new NetworkCredential("hoainam3183@gmail.com", "scuc bpjv iqdk kesi"),
            //    EnableSsl = true
            //};

            //USER user = (USER)Session["USERNAME"];
            //var username = db.CUSTOMERs.Where(u => u.ID == user.ID).FirstOrDefault();
            //var m = new MailMessage();
            //m.From = new MailAddress("hoainam3183@gmail.com");
            //m.ReplyToList.Add("hoainam3183@gmail.com");
            //m.To.Add(new MailAddress(user.EMAIL));
            //m.Subject = "Your booking has been completed !";
            //m.Body = "   Dear Mr/Mrs " + username.NAME + ",\n   This is your detail information of your booking: \n "
            //    + "Hotel: " + hotel.NAME + "\n"
            //    + "Check-in Date: " + f["checkIn"] + "\n"
            //    + "Check-out Date: " + f["checkOut"] + "\n"
            //    + "Room: " + f["room"] + "\n"
            //    + "Total Price: " + totalPrice + "\n"
            //    + "   Please be present at the hotel check-in time for check-in instructions ! \n   Best regards !";

            //mail.Send(m);

            return RedirectToAction("Stays", "HomePage");
        }
        //public ActionResult BookingPackage()
        //{
        //    return RedirectToAction("Index", "HomePage");
        //}
        //public ActionResult CompleteBookPackage(int IDPackage, FormCollection f)
        //{
        //    var userLogin = (CUSTOMER)Session["USERNAME"];
        //    if (userLogin == null)
        //    {
        //        return RedirectToAction("Login", "User");
        //    }
        //    else
        //    {
        //        var hotel = db.HOTELs.Where(h => h.ID == IDPackage).FirstOrDefault();
        //        var book = new BOOKINGHOTEL
        //        {
        //            IDHOTEL = hotel.ID,
        //            IDCUSTOMER = userLogin.ID,
        //            TOTALPRICE = hotel.PRICE_PER_PERSON,
        //            NUMOFPERSON = 1
        //        };
        //        db.BOOKINGHOTELs.Add(book);
        //        db.SaveChanges();

        //        var mail = new SmtpClient("smtp.gmail.com", 25)
        //        {
        //            Credentials = new NetworkCredential("hoainam3183@gmail.com", "scuc bpjv iqdk kesi"),
        //            EnableSsl = true
        //        };

        //        var m = new MailMessage();
        //        m.From = new MailAddress("hoainam3183@gmail.com");
        //        m.ReplyToList.Add("hoainam3183@gmail.com");
        //        m.To.Add(new MailAddress(f["To"]));
        //        m.Subject = "Your booking has been completed !";
        //        m.Body = "Dear Mr/Mrs " + f["LastName"] + ",\nThis is your detail information of your booking ! \n ";

        //        mail.Send(m);
        //    }

        //    return RedirectToAction("Index", "HomePage");
        //}
        public ActionResult BookingFlight(int IDFlight)
        {
            //var userLogin = (CUSTOMER)Session["USERNAME"];
            //if (userLogin == null)
            //{
            //    return RedirectToAction("Login", "User");
            //}
            //else
            //{
            //    var hotel = db.HOTELs.Where(h => h.ID == IDHotel).FirstOrDefault();
            //    return View(hotel);
            //}
            var flight = db.FLIGHT.Where(f => f.ID == IDFlight).FirstOrDefault();
            return View(flight);
        }

        public ActionResult CompleteBookFlight(FormCollection f)
        {
            var IDFlight = Int32.Parse(f["ID"]);
            var flight = db.FLIGHT.Where(h => h.ID == IDFlight).FirstOrDefault();
            double totalPrice = flight.PRICE_PER_PERSON * Int32.Parse(f["passenger"]);
            var book = new BOOKINGFLIGHT
            {
                IDFLIGHT = flight.ID,
                IDCUSTOMER = 6,
                BOOKINGDATE = DateTime.Now.Date,
                NUMOFPERSON = Int32.Parse(f["passenger"]),
                TOTALPRICE = totalPrice
            };
            db.BOOKINGFLIGHT.Add(book);
            db.SaveChanges();

            //var mail = new SmtpClient("smtp.gmail.com", 25)
            //{
            //    Credentials = new NetworkCredential("hoainam3183@gmail.com", "scuc bpjv iqdk kesi"),
            //    EnableSsl = true
            //};

            //USER user = (USER)Session["USERNAME"];
            //var username = db.CUSTOMERs.Where(u => u.ID == user.ID).FirstOrDefault();
            //var m = new MailMessage();
            //m.From = new MailAddress("hoainam3183@gmail.com");
            //m.ReplyToList.Add("hoainam3183@gmail.com");
            //m.To.Add(new MailAddress(user.EMAIL));
            //m.Subject = "Your booking has been completed !";
            //m.Body = "   Dear Mr/Mrs " + username.NAME + ",\n   This is your detail information of your booking: \n "
            //    + "Hotel: " + hotel.NAME + "\n"
            //    + "Check-in Date: " + f["checkIn"] + "\n"
            //    + "Check-out Date: " + f["checkOut"] + "\n"
            //    + "Room: " + f["room"] + "\n"
            //    + "Total Price: " + totalPrice + "\n"
            //    + "   Please be present at the hotel check-in time for check-in instructions ! \n   Best regards !";

            //mail.Send(m);

            return RedirectToAction("Flights", "HomePage");
        }

        public ActionResult BookingPackage(int idPackage)
        {
            //var userLogin = (CUSTOMER)Session["USERNAME"];
            //if (userLogin == null)
            //{
            //    return RedirectToAction("Login", "User");
            //}
            //else
            //{
            //    var package = db.PACKAGEs.Where(h => h.ID == idPackage).FirstOrDefault();
            //    return View(package);
            //}
            var package = db.PACKAGE.Where(h => h.ID == idPackage).FirstOrDefault();
            return View(package);
        }

        public ActionResult CompleteBookPackage(FormCollection f)
        {
            var IDPackage = Int32.Parse(f["ID"]);
            var package = db.PACKAGE.Where(h => h.ID == IDPackage).FirstOrDefault();
            double totalPrice = package.PRICE_PER_PERSON * Int32.Parse(f["guest"]);
            var book = new BOOKINGPACKAGE
            {
                IDCUSTOMER = 6,
                IDPACKAGE = IDPackage,
                BOOKINGDATE = DateTime.Now.Date,
                NUMOFPERSON = Int32.Parse(f["guest"]),
                TOTALPRICE = totalPrice
            };
            db.BOOKINGPACKAGE.Add(book);
            db.SaveChanges();

            return RedirectToAction("Packages", "HomePage");
        }

        public ActionResult PartialHotel(int id)
        {
            var hotel = db.HOTEL.Where(h => h.ID == id).FirstOrDefault();
            return PartialView(hotel);
        }
        public ActionResult PartialFlight(int id)
        {
            var flight = db.FLIGHT.Where(f => f.ID == id).FirstOrDefault();
            return PartialView(flight);
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
                var lstBHotel = db.BOOKINGHOTEL.Where(d => d.IDCUSTOMER == userLogin.ID).ToList();
                var lstBFlight = db.BOOKINGFLIGHT.Where(d => d.IDCUSTOMER == userLogin.ID).ToList();
                var lstBPackage = db.BOOKINGPACKAGE.Where(d => d.IDCUSTOMER == userLogin.ID).ToList();

                model.Add(lstBHotel);
                model.Add(lstBFlight);
                model.Add(lstBPackage);
            }
            return View(model);
        }
    }
}