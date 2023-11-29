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

namespace Final_Report.Areas.Admin.Controllers
{
    public class HotelController : Controller
    {
        // GET: Admin/Hotel
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
        [ValidateInput(false)]
        public ActionResult Create(HOTEL model,FormCollection f,HttpPostedFileBase fFileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    Image img=Image.FromStream(fFileUpload.InputStream,true,true);
                    model.PICTURES = Utility.ConvertImageToBase64(img);
                }
                db.HOTEL.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Detail(int id)
        {
            var hotel = db.HOTEL.SingleOrDefault(n => n.ID == id);
            if (hotel == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hotel);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var hotel = db.HOTEL.SingleOrDefault(n => n.ID == id);
            return View(hotel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(HOTEL model, HttpPostedFileBase fFileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    Image img=Image.FromStream(fFileUpload.InputStream,true,true);
                    model.PICTURES = Utility.ConvertImageToBase64(img);
                }
                db.HOTEL.AddOrUpdate(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var hotel = db.HOTEL.FirstOrDefault(s => s.ID == id);
            if (hotel == null)
            {
                TempData["Message"] = "Hotel is not exist";
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