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

namespace Final_Report.Areas.Admin.Controllers
{
    public class FlightController : Controller
    {
        // GET: Admin/Flight
        Model1 db = new Model1();
        public ActionResult Index(int? page)
        {
            var lstHotel = db.FLIGHT.OrderBy(s => s.ID);
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
        public ActionResult Create(FLIGHT model, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            if (ModelState.IsValid)
            {
                if (fFileUpload != null && fFileUpload.ContentLength > 0)
                {
                    // Validate file type (you may want to improve this check)
                    if (fFileUpload.ContentType.StartsWith("image"))
                    {
                        using (Image img = Image.FromStream(fFileUpload.InputStream, true, true))
                        {
                            model.PICTURES = Utility.ConvertImageToBase64(img);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("fFileUpload", "Invalid file type. Please upload an image.");
                        return View(model);
                    }
                }

                db.FLIGHT.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public ActionResult Detail(int id)
        {
            var flight = db.FLIGHT.SingleOrDefault(n => n.ID == id);
            if (flight == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(flight);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var flight = db.FLIGHT.SingleOrDefault(n => n.ID == id);
            if (flight == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(flight);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var flight = db.FLIGHT.SingleOrDefault(n => n.ID == int.Parse(f["iID"]));
            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    flight.PICTURES = sFileName;
                }
                flight.COMPANY = f["sCOMPANY"];
                flight.DEPARTURE = Convert.ToDateTime(f["sDEPART"]);
                flight.ARRIVAL = Convert.ToDateTime(f["sARRIVE"]);
                flight.MODIFIEDDAY = Convert.ToDateTime(f["dMODIFIEDDAY"]);
                flight.PRICE_PER_PERSON = float.Parse(f["sPRICE"]);
                flight.FROM = f["sFROM"];
                flight.TO = f["sTO"];
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            var flight = db.FLIGHT.FirstOrDefault(s => s.ID == id);
            if (flight == null)
            {
                TempData["Message"] = "Flight is not exist";
                return RedirectToAction("Index");
            }
            var package = db.BOOKINGPACKAGE.Where(tg => tg.IDFLIGHT == id);
            if (package.Count() > 0)
            {
                db.BOOKINGPACKAGE.RemoveRange(package);
                db.SaveChanges();
            }
            db.FLIGHT.Remove(flight);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}