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
    public class DepartmentController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();

        // GET: Department
        public ActionResult ViewAll()
        {
            return View(db.Departments.ToList());
        }

        // GET: Department/Create
        public ActionResult Save()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "Id,Code,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                ViewBag.SuccessMessage = "New Department Saved Successfully!";
            }
            return View();
        }

        public JsonResult IsDepartmentCodeExists(string code)
        {
            return Json(!db.Departments.Any(x => x.Code == code), JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsDepartmentNameExists(string name)
        {
            return Json(!db.Departments.Any(x => x.Name == name), JsonRequestBehavior.AllowGet);
        }
    }
}
