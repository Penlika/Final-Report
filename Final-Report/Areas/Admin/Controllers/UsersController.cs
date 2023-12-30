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
    public class UsersController : Controller
    {
        // GET: Admin/User
        Model1 db = new Model1();
        public ActionResult Index(int? page)
        {
            var lstHotel = db.CUSTOMER.OrderBy(s => s.ID);
            int pageNum = (page ?? 1);
            int pageSize = 7;
            return View(lstHotel.ToList().OrderBy(n => n.ID).ToPagedList(pageNum, pageSize));
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cus = db.CUSTOMER.SingleOrDefault(n => n.ID == id);
            return View(cus);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(CUSTOMER model, HttpPostedFileBase fFileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    Image img = Image.FromStream(fFileUpload.InputStream, true, true);
                    model.PICTURES = Utility.ConvertImageToBase64(img);
                }
                else
                {
                    CUSTOMER existingCus = db.CUSTOMER.FirstOrDefault(c => c.EMAIL == model.EMAIL);
                    if (existingCus != null)
                    {
                        model.PICTURES = existingCus.PICTURES;
                    }
                }
                db.CUSTOMER.AddOrUpdate(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Detail(int id)
        {
            var cus = db.CUSTOMER.SingleOrDefault(n => n.ID == id);
            return View(cus);
        }
        public ActionResult Delete(int id)
        {
            var cus = db.CUSTOMER.SingleOrDefault(n => n.ID == id);
            if (cus == null)
            {
                TempData["Message"] = "The customer did not exist";
                return RedirectToAction("Index");
            }
            var hotel = db.BOOKINGHOTEL.Where(ct => ct.IDCUSTOMER == id).ToList();
            if (hotel.Count() > 0)
            {
                TempData["Message"] = "The user has paid and is currently using our service";
                return RedirectToAction("Index");
            }
            var flight = db.BOOKINGFLIGHT.Where(ct => ct.IDCUSTOMER == id).ToList();
            if (hotel.Count() > 0)
            {
                TempData["Message"] = "The user has paid and is currently using our service";
                return RedirectToAction("Index");
            }
            var package = db.BOOKINGPACKAGE.Where(ct => ct.IDCUSTOMER == id).ToList();
            if (package.Count() > 0)
            {
                TempData["Message"] = "The user has paid and is currently using our service";
                return RedirectToAction("Index");
            }
            db.CUSTOMER.Remove(cus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}