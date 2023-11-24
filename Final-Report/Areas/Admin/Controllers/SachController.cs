using PagedList;
using Final_Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace SachOnline.Areas.Admin.Controllers
{
    public class SachController : Controller
    {
        // GET: Admin/Sach
        Model1 db = new Model1();
        public ActionResult Index(int? page)
        {
            var lstSaches = db.HOTEL.OrderBy(s => s.ID);
            int pageNum = (page ?? 1);
            int pageSize = 7;
            return View(db.HOTEL.ToList().OrderBy(n => n.ID).ToPagedList(pageNum, pageSize));
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
            var sach = db.HOTEL.FirstOrDefault(s => s.ID == id);
            if (sach == null)
            {
                TempData["Message"] = "Sách không tồn tại";
                return RedirectToAction("Index");
            }
            var ctdh = db.CHITIETDONTHANGs.Where(ct => ct.Masach == id).ToList();
            if (ctdh.Count() > 0)
            {
                TempData["Message"] = "Sách tồn tại đơn đặt hàng không thể xóa";
                return RedirectToAction("Index");
            }
            var vietSach = db.HOTEL.Where(tg => tg.ID == id);
            if (vietSach.Count() > 0)
            {
                db.HOTEL.RemoveRange(vietSach);
                db.SaveChanges();
            }
            db.HOTEL.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}