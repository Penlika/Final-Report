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
            var existingImages = db.HOTELIMG
                                .Where(img => img.IDHOTEL == id)
                                .Select(img => img.IMAGEURL)
                                .Take(6)
                                .ToList();
            ViewBag.ExistingImages = existingImages;
            return View(hotel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(HOTEL model, HttpPostedFileBase fFileUpload, List<string> imageUrls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (fFileUpload != null)
                    {
                        Image img = Image.FromStream(fFileUpload.InputStream, true, true);
                        model.PICTURE = Utility.ConvertImageToBase64(img);
                    }
                    else
                    {
                        HOTEL existingHotel = db.HOTEL.Find(model.ID);
                        if (existingHotel != null)
                        {
                            model.PICTURE = existingHotel.PICTURE;
                        }
                    }

                    // Update or add images to HOTELIMG table
                    UpdateHotelImages(model.ID, imageUrls);

                    db.HOTEL.AddOrUpdate(model);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception (replace with your preferred logging mechanism)
                Console.WriteLine($"Error during hotel update: {ex.Message}");
                // Optionally, handle the exception or redirect to an error page
                return RedirectToAction("Error");
            }
        }

        private void UpdateHotelImages(int hotelId, List<string> imageUrls)
        {
            // Remove existing images
            var existingImages = db.HOTELIMG.Where(img => img.IDHOTEL == hotelId).ToList();
            db.HOTELIMG.RemoveRange(existingImages);

            // Add new images
            if (imageUrls != null)
            {
                foreach (var imageUrl in imageUrls)
                {
                    db.HOTELIMG.Add(new HOTELIMG { IDHOTEL = hotelId, IMAGEURL = imageUrl });
                }
            }
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