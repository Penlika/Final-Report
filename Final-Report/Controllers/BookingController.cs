using FundamentalProject.Models;
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
        Model1 db=new Model1();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BookingPackage()
        {
            var userLogin = (CUSTOMER)Session["USERNAME"];
            if (userLogin == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                
            }

            return RedirectToAction("Index", "HomePage");
        }
        public ActionResult BookingHotel(int ID)
        {
            var userLogin = (CUSTOMER)Session["USERNAME"];
            if (userLogin == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {

            }

            return RedirectToAction("Index", "HomePage");
        }
        public ActionResult BookingFlight()
        {
            var userLogin = (CUSTOMER)Session["USERNAME"];
            if (userLogin == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {

            }

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