using Final_Report.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Data.Entity.Validation;

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
        public ActionResult Details(int hotelId)
        {
            var user = (CUSTOMER)Session["customer"];

            var hotel = db.HOTEL.FirstOrDefault(h => h.ID == hotelId);
            if (hotel == null)
            {
                // Handle the case where the hotel is not found
                return HttpNotFound();
            }

            var commentsAndRatings = db.COMMENTANDRATING
                .Where(cr => cr.IDHOTEL == hotelId)
                .OrderByDescending(cr => cr.DATECREATE)  // Order by date created
                .Select(cr => new CommentAndRatingViewModel
                {
                    CommentId = cr.ID,
                    Comment = cr.COMMENT,
                    StarRating = cr.STAR_RATING,
                    DateCreate = (DateTime)cr.DATECREATE,
                    UserId = (int)cr.IDCUSTOMER // Assuming IDCUSTOMER is the foreign key in the COMMENTANDRATING table
                })
                .ToList();

            // Explicitly load related CUSTOMER information
            foreach (var commentRating in commentsAndRatings)
            {
                var customer = db.CUSTOMER.Where(c => c.ID == commentRating.UserId).FirstOrDefault();
                commentRating.UserName = customer?.USERNAME;
                commentRating.UserPictureUrl = customer?.PICTURES;
            }

            var averageRating = commentsAndRatings.Any() ? commentsAndRatings.Average(cr => cr.StarRating) : 0;

            var userComment = user != null
                ? commentsAndRatings.FirstOrDefault(cr => cr.UserId == user.ID)
                : null;

            var model = new HotelDetailsViewModel
            {
                Hotel = hotel,
                CommentsAndRatings = commentsAndRatings,
                AverageRating = averageRating,
                UserHasCommented = userComment != null,
                UserComment = userComment?.Comment,
                UserRating = userComment?.StarRating ?? 0,
            };

            return View(model);
        }










        [HttpPost]
        public ActionResult AddOrUpdateCommentAndRating(int hotelId, string comment, int rating)
        {
            var user = (CUSTOMER)Session["customer"];

            if (user == null)
            {
                // Redirect to login page with a message
                return RedirectToAction("Login", "User", new { message = "Please log in to comment." });
            }

            var hotel = db.HOTEL.FirstOrDefault(h => h.ID == hotelId);

            var existingComment = db.COMMENTANDRATING
                .FirstOrDefault(cr => cr.IDHOTEL == hotelId && cr.IDCUSTOMER == user.ID);

            if (existingComment != null)
            {
                existingComment.COMMENT = comment;
                existingComment.STAR_RATING = rating;
                existingComment.DATECREATE = DateTime.Now;

                // Mark entity as modified
                db.Entry(existingComment).State = EntityState.Modified;
            }
            else
            {
                var newComment = new COMMENTANDRATING
                {
                    IDHOTEL = hotelId,
                    IDCUSTOMER = user.ID,
                    COMMENT = comment,
                    STAR_RATING = rating,
                    DATECREATE = DateTime.Now
                };

                // Add new comment
                db.COMMENTANDRATING.Add(newComment);
            }

            // Save changes to the database
            db.SaveChanges();

            // Redirect to the hotel details page
            return RedirectToAction("Details", new { hotelId });
        }

        [HttpPost]
        public ActionResult DeleteCommentAndRating(int commentId, int hotelId)
        {
            var userEmail = (string)Session["EMAIL"];
            var user = db.ACCOUNT.FirstOrDefault(a => a.EMAIL == userEmail);

            if (user != null)
            {
                var comment = db.COMMENTANDRATING.FirstOrDefault(cr =>cr.IDHOTEL == hotelId && cr.IDCUSTOMER == user.ID);

                db.COMMENTANDRATING.Remove(comment);
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { hotelId });

        }






        public ActionResult PartialComment()
        {
            return PartialView();
        }
        // Inside BookingController



        public ActionResult ConfirmInfo()
        {
            string userEmail = (string)Session["EMAIL"];

            // Retrieve customer data based on the email
            CUSTOMER customer = db.CUSTOMER.FirstOrDefault(c => c.EMAIL == userEmail);
            return View(customer);
        }
        public ActionResult ConfirmInfoFlight()
        {
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
            return View("ConfirmInfo",model);
        }

        public ActionResult Payment()
        {
            // Retrieve booking information from TempData
            var bookingInfo = TempData["BookingInfo"] as dynamic;

            // Pass the booking information to the view
            ViewBag.BookingInfo = bookingInfo;

            string userEmail = (string)Session["EMAIL"];
            // Retrieve customer data based on the email
            CUSTOMER customer = db.CUSTOMER.FirstOrDefault(c => c.EMAIL == userEmail);
            return View(customer);
        }
        public ActionResult PaymentFlight()
        {
            // Retrieve booking information from TempData
            var bookingInfo = TempData["BookingInfo"] as dynamic;

            // Pass the booking information to the view
            ViewBag.BookingInfo = bookingInfo;

            string userEmail = (string)Session["EMAIL"];
            // Retrieve customer data based on the email
            CUSTOMER customer = db.CUSTOMER.FirstOrDefault(c => c.EMAIL == userEmail);
            return View(customer);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPayment(CUSTOMER model)
        {
            if (ModelState.IsValid)
            {
                db.CUSTOMER.AddOrUpdate(model);
                db.SaveChanges();
            }
            return View("Payment", model);
        }


        public ActionResult BookingComplete()
        {
            return View();
        }

        public ActionResult BookingHotel(int IDHotel)
        {
            var userLogin = (CUSTOMER)Session["customer"];
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

        [HttpPost]
        public ActionResult CompleteBookHotel(FormCollection f)
        {
            CUSTOMER user = (CUSTOMER)Session["customer"];
            var IDHotel = Int32.Parse(f["ID"]);
            var hotel = db.HOTEL.Where(h => h.ID == IDHotel).FirstOrDefault();
            var updateHotel = db.HOTEL.Where(h => h.ID == hotel.ID).FirstOrDefault();
            
                double totalPrice = calTotalPriceHotel(hotel.PRICE_PER_PERSON, Int32.Parse(f["room"]), DateTime.Parse(f["checkIn"]), DateTime.Parse(f["checkOut"]));
                Session["BookingInfo"] = new
                {
                    IDHotel = hotel.ID,
                    IDCUSTOMER = user.ID,
                    CHECKINDATE = DateTime.Parse(f["checkIn"]),
                    CHECKOUTDATE = DateTime.Parse(f["checkOut"]),
                    NUMOFPERSON = Int32.Parse(f["guest"]),
                    ROOM = Int32.Parse(f["room"]),
                    TOTALPRICE = totalPrice
                };


                int temp = updateHotel.ROOM_AVAILABLE;
                updateHotel.ROOM_AVAILABLE = temp - Int32.Parse(f["room"]);
                db.HOTEL.AddOrUpdate(updateHotel);
                db.SaveChanges();

                TempData["HotelName"] = hotel.NAME;
                TempData["CheckInDate"] = f["checkIn"];
                TempData["CheckOutDate"] = f["checkOut"];
                TempData["Room"] = f["room"];
                TempData["TotalPrice"] = totalPrice;

                return RedirectToAction("ConfirmInfo");
            
        }


        [HttpPost]
        public ActionResult CompleteBookFlight(FormCollection f)
        {
            var user = (CUSTOMER)Session["customer"];
            var IDFlight = Int32.Parse(f["ID"]);
            var flight = db.FLIGHT.Where(h => h.ID == IDFlight).FirstOrDefault();

            double totalPrice = flight.PRICE_PER_PERSON * Int32.Parse(f["passenger"]);

            // Store flight booking information in Session
            Session["BookingInfo"] = new
            {
                IDFLIGHT = flight.ID,
                IDCUSTOMER = user.ID,
                BOOKINGDATE = DateTime.Now.Date,
                NUMOFPERSON = Int32.Parse(f["passenger"]),
                TOTALPRICE = totalPrice,
                COMPANY = flight.COMPANY
            };

            TempData["CompanyName"] = flight.COMPANY;
            TempData["BookingDate"] = DateTime.Now.Date;
            TempData["NumberOfPersons"] = Int32.Parse(f["passenger"]);
            TempData["TotalPrice"] = totalPrice;

            return RedirectToAction("ConfirmInfoFlight");
        }


        [HttpPost]
        public ActionResult MakePayment()
        {
            // Retrieve booking information from Session
            var bookingInfo = Session["BookingInfo"] as dynamic;

            // Check if bookingInfo is not null
            if (bookingInfo != null)
            {
                // Insert the booking information into the SQL table
                var booking = new BOOKINGHOTEL
                {
                    IDHOTEL = bookingInfo.IDHotel,
                    IDCUSTOMER = bookingInfo.IDCUSTOMER,
                    CHECKINDATE = bookingInfo.CHECKINDATE,
                    CHECKOUTDATE = bookingInfo.CHECKOUTDATE,
                    NUMOFPERSON = bookingInfo.NUMOFPERSON,
                    ROOM = bookingInfo.ROOM,
                    TOTALPRICE = bookingInfo.TOTALPRICE
                };

                db.BOOKINGHOTEL.Add(booking);
                db.SaveChanges();
                var user = db.ACCOUNT.Find(bookingInfo.IDCUSTOMER);
                var hotel = db.HOTEL.Find(bookingInfo.IDHotel);
                SendBookingConfirmationEmail(user, hotel, bookingInfo.CHECKINDATE.ToString(), bookingInfo.CHECKOUTDATE.ToString(), bookingInfo.ROOM.ToString(), bookingInfo.TOTALPRICE);
                // Clear Session to prevent reusing the booking information
                Session["BookingInfo"] = null;

                // Return the final view
                return View("BookingComplete");
            }
            else
            {
                // Handle the case where bookingInfo is null
                // Redirect to an appropriate view or take necessary action
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult MakePaymentFlight()
        {
            // Retrieve booking information from Session
            var bookingInfo = Session["BookingInfo"] as dynamic;

            // Check if bookingInfo is not null
            if (bookingInfo != null)
            {
                // Insert the booking information into the SQL table
                var booking = new BOOKINGFLIGHT  // Assuming you have a BOOKINGFLIGHT entity
                {
                    IDFLIGHT = bookingInfo.IDFLIGHT,  // Assuming IDFLIGHT is the correct property
                    IDCUSTOMER = bookingInfo.IDCUSTOMER,
                    BOOKINGDATE = bookingInfo.BOOKINGDATE,
                    NUMOFPERSON = bookingInfo.NUMOFPERSON,
                    TOTALPRICE = bookingInfo.TOTALPRICE
                };

                db.BOOKINGFLIGHT.Add(booking);
                db.SaveChanges();

                var user = db.ACCOUNT.Find(bookingInfo.IDCUSTOMER);
                var flight = db.FLIGHT.Find(bookingInfo.IDFLIGHT);

                SendBookingConfirmationEmailFlight(user, flight, bookingInfo.NUMOFPERSON, bookingInfo.TOTALPRICE);
                // Clear Session to prevent reusing the booking information
                Session["BookingInfo"] = null;

                // Return the final view
                return View("BookingComplete");
            }
            else
            {
                // Handle the case where bookingInfo is null
                // Redirect to an appropriate view or take necessary action
                return RedirectToAction("Index", "Home");
            }
        }

        private void SendBookingConfirmationEmail(ACCOUNT user, HOTEL hotel, string checkIn, string checkOut, string room, double totalPrice)
        {
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
            m.Subject = "Your booking has been completed!";
            m.Body = "Dear Mr/Mrs " + username.NAME + ",\nThis is your detail information of your booking:\n"
                    + "Hotel: " + hotel.NAME + "\n"
                    + "Check-in Date: " + checkIn + "\n"
                    + "Check-out Date: " + checkOut + "\n"
                    + "Room: " + room + "\n"
                    + "Total Price: " + totalPrice + "\n"
                    + "Please be present at the hotel check-in time for check-in instructions !\nBest regards !";

            mail.Send(m);
        }

        private void SendBookingConfirmationEmailFlight(ACCOUNT user, FLIGHT flight, int numOfPersons, double totalPrice)
        {
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
            m.Subject = "Your booking has been completed!";
            m.Body = "Dear Mr/Mrs " + username.NAME + ",\nThis is your detail information of your booking: \n "
                + "Airline: " + flight.COMPANY + "\n"
                + "Departure: " + flight.DEPARTURE + "\n"
                + "Arrival: " + flight.ARRIVAL + "\n"
                + "From: " + flight.FROM + "\n"
                + "To: " + flight.TO + "\n"
                + "Passengers: " + numOfPersons + "\n"
                + "Total Price: " + totalPrice + "\n"
                + "Please be on time at the airport to check in for your flight!\nBest regards!";

            mail.Send(m);
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
            var userLogin = (CUSTOMER)Session["customer"];
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

        public ActionResult BookingPackage(int idPackage)
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