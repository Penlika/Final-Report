using Final_Report.Areas.Navigation;
using Final_Report.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Final_Report.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        Model1 db=new Model1();
        // GET: Admin/Home
        public ActionResult Index(string searchString, int? page)
        {
            var admin = db.ADMIN.FirstOrDefault();
            Session["Admin"] = admin;
            searchString = searchString ?? "";
            var lstBook = db.HOTEL.Where(s => s.NAME.Contains(searchString)).OrderBy(s => s.ID);
            int pageNumber = (page) ?? 1;
            int pageSize = 6;
            return View(lstBook.ToPagedList(pageNumber, pageSize));
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
            return RedirectToAction("Index", "HomePage");
        }
    }
}