using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Report.Controllers
{
    public class HomePageController : Controller
    {
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
        public ActionResult ItemPage()
        {
            return View();
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
    }
}