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
        public ActionResult Create(HOTEL model, HttpPostedFileBase[] fFileUploads)
        {
            if (ModelState.IsValid)
            {
                HOTEL newHotel = new HOTEL();

                if (fFileUploads != null && fFileUploads.Length > 0)
                {
                    for (int i = 0; i < fFileUploads.Length; i++)
                    {
                        var fileUpload = fFileUploads[i];

                        if (fileUpload != null)
                        {
                            Image img = Image.FromStream(fileUpload.InputStream, true, true);
                            switch (i)
                            {
                                case 0: newHotel.PICTURE1 = Utility.ConvertImageToBase64(img); break;
                                case 1: newHotel.PICTURE2 = Utility.ConvertImageToBase64(img); break;
                                case 2: newHotel.PICTURE3 = Utility.ConvertImageToBase64(img); break;
                                case 3: newHotel.PICTURE4 = Utility.ConvertImageToBase64(img); break;
                                case 4: newHotel.PICTURE5 = Utility.ConvertImageToBase64(img); break;
                                case 5: newHotel.PICTURE6 = Utility.ConvertImageToBase64(img); break;
                            }
                        }
                    }
                }
                newHotel.NAME = model.NAME;
                newHotel.ADDRESS = model.ADDRESS;
                db.HOTEL.Add(newHotel);
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
        public ActionResult Edit(HOTEL model, HttpPostedFileBase[] fFileUploads)
        {
            if (ModelState.IsValid)
            {
                if (fFileUploads != null)
                {
                    for (int i = 0; i < fFileUploads.Length; i++)
                    {
                        var fileUpload = fFileUploads[i];

                        if (fileUpload != null)
                        {
                            Image img = Image.FromStream(fileUpload.InputStream, true, true);
                            switch (i)
                            {
                                case 0: model.PICTURE1 = Utility.ConvertImageToBase64(img); break;
                                case 1: model.PICTURE2 = Utility.ConvertImageToBase64(img); break;
                                case 2: model.PICTURE3 = Utility.ConvertImageToBase64(img); break;
                                case 3: model.PICTURE4 = Utility.ConvertImageToBase64(img); break;
                                case 4: model.PICTURE5 = Utility.ConvertImageToBase64(img); break;
                                case 5: model.PICTURE6 = Utility.ConvertImageToBase64(img); break;
                            }
                        }
                    }
                }

                // Retrieve the existing hotel from the database
                HOTEL existingHotel = db.HOTEL.FirstOrDefault(h => h.ID == model.ID);
                if (existingHotel != null)
                {
                    // Detach the existing entity
                    db.Entry(existingHotel).State = EntityState.Detached;

                    // Retain unchanged values
                    model.PICTURE1 = model.PICTURE1 ?? existingHotel.PICTURE1;
                    model.PICTURE2 = model.PICTURE2 ?? existingHotel.PICTURE2;
                    model.PICTURE3 = model.PICTURE3 ?? existingHotel.PICTURE3;
                    model.PICTURE4 = model.PICTURE4 ?? existingHotel.PICTURE4;
                    model.PICTURE5 = model.PICTURE5 ?? existingHotel.PICTURE5;
                    model.PICTURE6 = model.PICTURE6 ?? existingHotel.PICTURE6;

                    // Retain other unchanged properties
                    model.PRICE_PER_PERSON = model.PRICE_PER_PERSON != existingHotel.PRICE_PER_PERSON
                        ? model.PRICE_PER_PERSON
                        : existingHotel.PRICE_PER_PERSON;

                    model.ROOM_AVAILABLE = model.ROOM_AVAILABLE != existingHotel.ROOM_AVAILABLE
                        ? model.ROOM_AVAILABLE
                        : existingHotel.ROOM_AVAILABLE;
                }

                // Attach the modified entity
                db.Entry(model).State = EntityState.Modified;
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
            var package = db.PACKAGE.Where(tg => tg.IDHOTEL == id);
            if (package.Count() > 0)
            {
                db.PACKAGE.RemoveRange(package);
                db.SaveChanges();
            }
            db.HOTEL.Remove(hotel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}