using Final_Report.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public ActionResult BookingComplete()
        {
            return View();
        }
        public ActionResult BookingHotel(int IDHotel)
        {
            var userLogin = (ACCOUNT)Session["EMAIL"];
            if (userLogin == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var hotel = db.HOTEL.Where(h => h.ID == IDHotel).FirstOrDefault();
                return View(hotel);
            }
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
            ACCOUNT user = (ACCOUNT)Session["EMAIL"];
            var IDHotel = Int32.Parse(f["ID"]);
            var hotel = db.HOTEL.Where(h => h.ID == IDHotel).FirstOrDefault();
            var updateHotel = db.HOTEL.Where(h => h.ID == hotel.ID).FirstOrDefault();

                double totalPrice = calTotalPriceHotel(hotel.PRICE_PER_PERSON, Int32.Parse(f["room"]), DateTime.Parse(f["checkIn"]), DateTime.Parse(f["checkOut"]));
                var book = new BOOKINGHOTEL
                {
                    IDHOTEL = hotel.ID,
                    IDCUSTOMER = user.ID,
                    CHECKINDATE = DateTime.Parse(f["checkIn"]),
                    CHECKOUTDATE = DateTime.Parse(f["checkOut"]),
                    NUMOFPERSON = Int32.Parse(f["guest"]),
                    ROOM = Int32.Parse(f["room"]),
                    TOTALPRICE = totalPrice
                };
                db.BOOKINGHOTEL.Add(book);

                int temp = updateHotel.ROOM_AVAILABLE;
                updateHotel.ROOM_AVAILABLE = temp - Int32.Parse(f["room"]);
                db.HOTEL.AddOrUpdate(updateHotel);
                db.SaveChanges();

                var mail = new SmtpClient("smtp.gmail.com", 25)
                {
                    Credentials = new NetworkCredential("hoainam3183@gmail.com", "scuc bpjv iqdk kesi"),
                    EnableSsl = true
                };

                var username = db.CUSTOMER.Where(u => u.ID == user.ID).FirstOrDefault();
                var m = new MailMessage();
                m.From = new MailAddress("hoainam3183@gmail.com");
                m.ReplyToList.Add("hoainam3183@gmail.com");
                m.To.Add(new MailAddress(user.EMAIL));
                m.Subject = "Your booking has been completed !";
                m.Body = "Dear Mr/Mrs " + username.NAME + ",\nThis is your detail information of your booking:\n"
                    + "Hotel: " + hotel.NAME + "\n"
                    + "Check-in Date: " + f["checkIn"] + "\n"
                    + "Check-out Date: " + f["checkOut"] + "\n"
                    + "Room: " + f["room"] + "\n"
                    + "Total Price: " + totalPrice + "\n"
                    + "Please be present at the hotel check-in time for check-in instructions !\nBest regards !";

                mail.Send(m);

                return View("BookingComplete");
            
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
            var userLogin = (ACCOUNT)Session["EMAIL"];
            if (userLogin == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var flight = db.FLIGHT.Where(f => f.ID == IDFlight).FirstOrDefault();
                return View(flight);
            }
        }

        public ActionResult CompleteBookFlight(FormCollection f)
        {
            var user = (ACCOUNT)Session["EMAIL"];
            var IDFlight = Int32.Parse(f["ID"]);
            var flight = db.FLIGHT.Where(h => h.ID == IDFlight).FirstOrDefault();
            double totalPrice = flight.PRICE_PER_PERSON * Int32.Parse(f["passenger"]);
            var book = new BOOKINGFLIGHT
            {
                IDFLIGHT = flight.ID,
                IDCUSTOMER = user.ID,
                BOOKINGDATE = DateTime.Now.Date,
                NUMOFPERSON = Int32.Parse(f["passenger"]),
                TOTALPRICE = totalPrice
            };
            db.BOOKINGFLIGHT.Add(book);
            db.SaveChanges();

            var mail = new SmtpClient("smtp.gmail.com", 25)
            {
                Credentials = new NetworkCredential("hoainam3183@gmail.com", "scuc bpjv iqdk kesi"),
                EnableSsl = true
            };

            var username = db.CUSTOMER.Where(u => u.ID == user.ID).FirstOrDefault();
            var m = new MailMessage();
            m.From = new MailAddress("hoainam3183@gmail.com");
            m.ReplyToList.Add("hoainam3183@gmail.com");
            m.To.Add(new MailAddress(user.EMAIL));
            m.Subject = "Your booking has been completed !";
            m.Body = "Dear Mr/Mrs " + username.NAME + ",\nThis is your detail information of your booking: \n "
                + "Airline: " + flight.COMPANY + "\n"
                + "Departure: " + flight.DEPARTURE + "\n"
                + "Arrival: " + flight.ARRIVAL + "\n"
                + "From: " + flight.FROM + "\n"
                + "To: " + flight.TO + "\n"
                + "Passensers: " + f["passenger"] + "\n"
                + "Total Price: " + totalPrice + "\n"
                + "Please be on time at the airport to check in for your flight !\nBest regards !";

            mail.Send(m);

            return View("BookingComplete");
        }

        //public ActionResult BookingPackage(int idPackage)
        //{
        //    var userLogin = (ACCOUNT)Session["EMAIL"];
        //    if (userLogin == null)
        //    {
        //        return RedirectToAction("Login", "User");
        //    }
        //    else
        //    {
        //        var package = db.PACKAGE.Where(h => h.ID == idPackage).FirstOrDefault();
        //        return View(package);
        //    }
        //}

        [HttpPost]
        public ActionResult ChooseHotel(string id)
        {
            ViewBag.IDHotel = id;
            try
            {
                int idhotel = Convert.ToInt32(id);
                var hotel = db.HOTEL.Where(h => h.ID == idhotel).FirstOrDefault();
                return PartialView("PartialHotel", hotel);
            }
            catch { }
            return View();
        }
        public ActionResult PartialHotel(HOTEL hotel)
        {
            return PartialView(hotel);
        }
        public ActionResult PartialFlight(int id)
        {
            var flight = db.FLIGHT.Where(f => f.ID == id).FirstOrDefault();
            return PartialView(flight);
        }

        //public ActionResult CompleteBookPackage(FormCollection f)
        //{
        //    var user = (ACCOUNT)Session["EMAIL"];

        //    var hotel = db.HOTEL.Where(h => h.ID == package.IDHOTEL).FirstOrDefault();
        //    var flight = db.FLIGHT.Where(fl => fl.ID == package.IDFLIGHT).FirstOrDefault();
        //    double totalPrice = package.PRICE_PER_PERSON * Int32.Parse(f["guest"]);
        //    var book = new BOOKINGPACKAGE
        //    {
        //        IDCUSTOMER = user.ID,
        //        IDPACKAGE = IDPackage,
        //        BOOKINGDATE = DateTime.Now.Date,
        //        NUMOFPERSON = Int32.Parse(f["guest"]),
        //        TOTALPRICE = totalPrice
        //    };
        //    db.BOOKINGPACKAGE.Add(book);
        //    var updateHotel = db.HOTEL.Where(h => h.ID == hotel.ID).FirstOrDefault();
        //    int temp = updateHotel.ROOM_AVAILABLE;
        //    updateHotel.ROOM_AVAILABLE = temp - Int32.Parse(f["guest"]);
        //    db.HOTEL.AddOrUpdate(updateHotel);
        //    db.SaveChanges();

        //    var mail = new SmtpClient("smtp.gmail.com", 25)
        //    {
        //        Credentials = new NetworkCredential("hoainam3183@gmail.com", "scuc bpjv iqdk kesi"),
        //        EnableSsl = true
        //    };

        //    var username = db.CUSTOMER.Where(u => u.ID == user.ID).FirstOrDefault();
        //    var m = new MailMessage();
        //    m.From = new MailAddress("hoainam3183@gmail.com");
        //    m.ReplyToList.Add("hoainam3183@gmail.com");
        //    m.To.Add(new MailAddress(user.EMAIL));
        //    m.Subject = "Your booking has been completed !";
        //    m.Body = "Dear Mr/Mrs " + username.NAME + ",\nThis is your detail information of your booking:\n "
        //        + "#About your Hotel: \n"
        //        + "Hotel: " + hotel.NAME + "\n"
        //        + "Address: " + hotel.ADDRESS + "\n"
        //        + "#About your Flight: \n"
        //        + "Airline: " + flight.COMPANY + "\n"
        //        + "Departure: " + flight.DEPARTURE + "\n"
        //        + "Arrival: " + flight.ARRIVAL + "\n"
        //        + "From: " + flight.FROM + "\n"
        //        + "To: " + flight.TO + "\n"
        //        + "Guests: " + f["guest"] + "\n"
        //        + "Total Price: " + totalPrice + "\n"
        //        + "Please be on time at the airport to check in for your flight !\nBest regards !";

        //    mail.Send(m);

        //    return View("BookingComplete");
        //}


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