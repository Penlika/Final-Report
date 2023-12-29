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
using System.Data.Entity;

namespace Final_Report.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        Model1 db = new Model1();
        public ActionResult Index(int? page)
        {
            var lstHotel = db.MANAGER.OrderBy(s => s.ID);
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
        public ActionResult Create(ACCOUNT model)
        {
            if (ModelState.IsValid)
            {
                model.ROLE = "manager";
                db.ACCOUNT.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cus = db.MANAGER.SingleOrDefault(n => n.ID == id);
            return View(cus);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(MANAGER model, HttpPostedFileBase fFileUpload)
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
                    MANAGER existingCus = db.MANAGER.FirstOrDefault(c => c.EMAIL == model.EMAIL);
                    if (existingCus != null)
                    {
                        model.PICTURES = existingCus.PICTURES;
                    }
                }
                db.MANAGER.AddOrUpdate(model);
                db.SaveChanges();
            }
            return RedirectToAction("Index",model);
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var ad = db.MANAGER.SingleOrDefault(n => n.ID == id);
            return View(ad);
        }
        public ActionResult Delete(int id)
        {
            var ad = db.MANAGER.FirstOrDefault(s => s.ID == id);
            if (ad == null)
            {
                TempData["Message"] = "Manager don't exist";
                return RedirectToAction("Index");
            }
            db.MANAGER.Remove(ad);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Profile()
        {
            string userEmail = (string)Session["EMAIL"];

            MANAGER man = db.MANAGER.FirstOrDefault(c => c.EMAIL == userEmail);
            return View(man);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProfile(MANAGER model, HttpPostedFileBase fFileUpload)
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
                    MANAGER existingCus = db.MANAGER.FirstOrDefault(c => c.EMAIL == model.EMAIL);
                    if (existingCus != null)
                    {
                        model.PICTURES = existingCus.PICTURES;
                    }
                }
                db.MANAGER.AddOrUpdate(model);
                db.SaveChanges();
            }
            return RedirectToAction("Profile", model);
        }
    }
}