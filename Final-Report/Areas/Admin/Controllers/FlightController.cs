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
        public ActionResult GetLogo(string company)
        {
            var logoUrl = db.LOGOIMG.FirstOrDefault(l => l.COMPANY == company)?.LOGO;
            return Content(logoUrl ?? "");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CompanyList = new SelectList(db.LOGOIMG.ToList().OrderBy(n => n.COMPANY), "COMPANY", "COMPANY");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FLIGHT model, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                // Set the logo based on the selected company
                var company = (f["COMPANY"]);
                model.LOGO = db.LOGOIMG.FirstOrDefault(l => l.COMPANY == company)?.LOGO;

                db.FLIGHT.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate the dropdown list if validation fails
            ViewBag.CompanyList = new SelectList(db.LOGOIMG.ToList().OrderBy(n => n.COMPANY), "COMPANY", "COMPANY", model.COMPANY);
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
        public ActionResult Edit(FormCollection f)
        {
            var flight = db.FLIGHT.SingleOrDefault(n => n.ID == int.Parse(f["iID"]));
            if (ModelState.IsValid)
            {
                flight.DEPARTURE = Convert.ToDateTime(f["sDEPART"]);
                flight.ARRIVAL = Convert.ToDateTime(f["sARRIVE"]);
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
            var booked = db.BOOKINGFLIGHT.Where(ct => ct.IDFLIGHT == id).ToList();
            if (booked.Count() > 0)
            {
                TempData["Message"] = "the service is currently book and cannot be delete";
                return RedirectToAction("Index");
            }
            var package = db.PACKAGE.Where(tg => tg.IDFLIGHT == id);
            if (package.Count() > 0)
            {
                db.PACKAGE.RemoveRange(package);
                db.SaveChanges();
            }
            db.FLIGHT.Remove(flight);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}