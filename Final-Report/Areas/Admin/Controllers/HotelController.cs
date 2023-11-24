using PagedList;
using Final_Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Final_Report.Areas.Admin.Controllers
{
    public class HotelController : Controller
    {
        // GET: Admin/Sach
        Model1 db = new Model1();
        public ActionResult Index(int? page)
        {
            var lstHotel = db.HOTEL.OrderBy(s => s.ID);
            int pageNum = (page ?? 1);
            int pageSize = 7;
            return View(lstHotel.ToList().OrderBy(n => n.ID).ToPagedList(pageNum, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(HOTEL model)
        {
            if (ModelState.IsValid)
            {
                model.BOOKINGHOTEL = ViewBag.MoTa;
                db.HOTEL.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        //public ActionResult Detail(int id)
        //{

        //}
        //public ActionResult Edit(int id)
        //{

        //}
        public ActionResult Delete(int id)
        {
            var hotel = db.HOTEL.FirstOrDefault(s => s.ID == id);
            if (hotel == null)
            {
                TempData["Message"] = "Sách không tồn tại";
                return RedirectToAction("Index");
            }
            var package = db.BOOKINGPACKAGE.Where(tg => tg.IDHOTEL == id);
            if (package.Count() > 0)
            {
                db.BOOKINGPACKAGE.RemoveRange(package);
                db.SaveChanges();
            }
            db.HOTEL.Remove(hotel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}