using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UCRMS_Version2.Models;

namespace UCRMS_Version2.Controllers
{
    public class TeacherController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        public ActionResult Save()
        {
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Designations = db.Designations.ToList();
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "Id,Name,Address,Email,ContactNo,CreditToBeTaken,DesignationId,DepartmentId,RemainingCredit")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.RemainingCredit = teacher.CreditToBeTaken;
                db.Teachers.Add(teacher);
                db.SaveChanges();
                ViewBag.SuccessMessage = "New Teacher Saved Successfully!";
            }
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Designations = db.Designations.ToList();
            return View();
        }

        public JsonResult IsTeacherEmailExists(string email)
        {
            return Json(!db.Teachers.Any(x => x.Email == email), JsonRequestBehavior.AllowGet);
        }
    }
}
