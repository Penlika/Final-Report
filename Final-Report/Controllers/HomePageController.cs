using Final_Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;

namespace Final_Report.Controllers
{
    public class HomePageController : Controller
    {
        Final db=new Final();
        // GET: HomePage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NavBar()
        { 
            return PartialView(); 
        }
        public ActionResult NavBarDetail()
        {
            return PartialView();
        }
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
        public ActionResult Stays(int? page)
        {
            var lstPACKAGE = db.PACKAGES.OrderBy(s => s.DESTINATION);
            int pageNumber = (page) ?? 1;
            int pageSize = 10;
            return View(lstPACKAGE.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult NavBarItem()
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
        public ActionResult Flights(int? page)
        {
            var lstPACKAGE = db.PACKAGES.OrderBy(s => s.DESTINATION);
            int pageNumber = (page) ?? 1;
            int pageSize = 10;
            return View(lstPACKAGE.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Packages()
        {
            return View();
        }
    }
}