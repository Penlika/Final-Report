using Final_Report.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Report.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        Final db=new Final();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult History()
        {
            return View();
        }
        public ActionResult UpdateCart(int IDPackages, int IDCustomer, int NumOfPerson)
        {
            var ct = db.BOOKING_FORM.FirstOrDefault(t => t.IDPACKAGE == IDPackages && t.IDCUSTOMER == IDCustomer);
            if (NumOfPerson == 0)
            {
                db.BOOKING_FORM.Remove(ct);
            }
            else
            {
                ct.NUMOFPERSON = NumOfPerson;
                db.BOOKING_FORM.AddOrUpdate(ct);
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}