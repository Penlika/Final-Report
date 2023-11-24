using Newtonsoft.Json;
using Final_Report.Areas.Navigation;
using Final_Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SachOnline.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login");
            return View();
        }
        public ActionResult MenuPartial()
        {
            var path = Server.MapPath("~/Areas/Navigation/Navigation.json");
            var content = System.IO.File.ReadAllText(path);
            var menu = JsonConvert.DeserializeObject<NavigationMenu>(content);
            return PartialView(menu);
        }
        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return RedirectToAction("Index");
        }
    }
}