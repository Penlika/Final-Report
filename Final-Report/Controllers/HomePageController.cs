using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Final_Report.Models;
using PagedList;
using PagedList.Mvc;

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
            var lstHotel = db.HOTELs.OrderBy(s => s.ID);
            int pageNumber = (page) ?? 1;
            int pageSize = 5;
            return View(lstHotel.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult BookingHotel()
        {
            return View();
        }
        //public ActionResult Packages(int? page)
        //{
        //    var lstPackage = db.PACKAGEs.OrderBy(s => s.ID);
        //    int pageNumber = (page) ?? 1;
        //    int pageSize = 6;
        //    return View(lstPackage.ToPagedList(pageNumber, pageSize));
        //}
        public ActionResult Flights(int? page)
        {
            var lstPACKAGE = db.FLIGHTs.OrderBy(s => s.ID);
            int pageNumber = (page) ?? 1;
            int pageSize = 5;
            return View(lstPACKAGE.ToPagedList(pageNumber, pageSize));
        }
        // ----------------- Partial View -----------------

        // ----------------- NavBar -----------------
        public ActionResult NavBar()
        { 
            return PartialView(); 
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
        public ActionResult TrendLocaPartial()
        {
            return PartialView();
        }
        public ActionResult FooterPartial()
        {
            return PartialView();
        }
        public ActionResult NewLocaPartial()
        {
            return PartialView();
        }
        public ActionResult TophotelPartial()
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
        
        
        public ActionResult test()
        {
            return View();
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