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
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
    }
}