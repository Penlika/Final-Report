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
    public class HomePageController : Controller
    {
        Model1 db=new Model1();
        // GET: HomePage
        // ----------------- View -----------------
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Stays(int? page)
        {
            var lstHotel = db.HOTEL.OrderBy(s => s.ID);
            int pageNumber = (page) ?? 1;
            int pageSize = 5;
            return View(lstHotel.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Packages(int? page)
        {
            var lstPackage = db.PACKAGE.OrderBy(s => s.ID);
            int pageNumber = (page) ?? 1;
            int pageSize = 6;
            return View(lstPackage.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Flights(int? page)
        {
            var lstPACKAGE = db.FLIGHT.OrderBy(s => s.ID);
            int pageNumber = (page) ?? 1;
            int pageSize = 5;
            return View(lstPACKAGE.ToPagedList(pageNumber, pageSize));
        }
        // ----------------- Partial View -----------------

        // ----------------- NavBar -----------------
        public ActionResult NavBar()
        { 
            CUSTOMER model = new CUSTOMER();
            return PartialView(model); 
        }
        public ActionResult NavBarDetail()
        {
            return PartialView();
        }
        public ActionResult NavBarItem()
        {
            return PartialView();
        }
        // ----------------- -----------------
        public ActionResult Top2()
        {
            return PartialView();
        }
        public ActionResult FooterPartial()
        {
            return PartialView();
        }
        public ActionResult Top1()
        {
            return PartialView();
        }
        public ActionResult Top3()
        {
            return PartialView();
        }
        public ActionResult InforhotelPartial()
        {
            return PartialView();
        }
        public ActionResult ListItem()
        {
            return PartialView();
        }
        public ActionResult SortItem()
        {
            return PartialView();
        }


        public ActionResult Step2(int IDHotel)
        {
            var userLogin = (CUSTOMER)Session["customer"];
            if (userLogin == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewData["IDHotel"] = IDHotel;  // Set ViewData for IDHotel
                var hotel = db.HOTEL.Where(h => h.ID == IDHotel).FirstOrDefault();
                return View(hotel);
            }
        }


        public ActionResult fillForm()
        {
            return PartialView();
        }
        public ActionResult TrendingLocation()
        {
            return PartialView();
        }
    }
}