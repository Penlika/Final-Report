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
    public class CommentAndRatingController : Controller
    {
        // GET: Admin/CommentAndRating
        Model1 db = new Model1();
        public ActionResult Index(int? page)
        {
            var lstCar = db.COMMENTANDRATING.OrderBy(s => s.ID);
            int pageNum = (page ?? 1);
            int pageSize = 7;
            return View(lstCar.ToList().OrderBy(n => n.ID).ToPagedList(pageNum, pageSize));
        }
        public ActionResult Detail(int id)
        {
            var car = db.COMMENTANDRATING.SingleOrDefault(n => n.ID == id);
            if (car == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(car);
        }
        public ActionResult Delete(int id)
        {
            var car = db.COMMENTANDRATING.FirstOrDefault(s => s.ID == id);
            if (car == null)
            {
                TempData["Message"] = "The comment and rating is not exist";
                return RedirectToAction("Index");
            }
            db.COMMENTANDRATING.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}