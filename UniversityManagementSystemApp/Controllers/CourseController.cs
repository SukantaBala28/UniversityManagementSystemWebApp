using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UCRMS_Version2.BLL;
using UCRMS_Version2.Models;

namespace UCRMS_Version2.Controllers
{
    public class CourseController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();
        CourseManager courseManager = new CourseManager();
        public ActionResult Save()
        {
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Semesters = db.Semesters.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "Id,Code,Name,Credit,Description,DepartmentId,SemesterId, TeacherId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                ViewBag.SuccessMessage = "New Course Saved Successfully!";
            }
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Semesters = db.Semesters.ToList();
            return View();
        }

        public JsonResult IsCourseCodeExists(string code)
        {
            return Json(!db.Courses.Any(x => x.Code == code), JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsCourseNameExists(string name)
        {
            return Json(!db.Courses.Any(x => x.Name == name), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AssignToTeacher()
        {
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignToTeacher(int? departmentId, int? teacherId, int? courseId)
        {
            if (departmentId != null && teacherId != null && courseId != null)
            {
                Course getCourseById = db.Courses.Single(x => x.Id == courseId);
                Teacher getTeacherById = db.Teachers.Single(t => t.Id == teacherId);

                if (getCourseById.TeacherId == null)
                {
                    getCourseById.TeacherId = teacherId;
                    if (ModelState.IsValid)
                    {
                        db.Entry(getCourseById).State = EntityState.Modified;
                        db.Entry(getTeacherById).State = EntityState.Modified;
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException ex) { }
                    }
                    ViewBag.SuccessMessage = "Course assigned Successfully!";
                }else
                {
                    ViewBag.FailMessage = "This course has already been assigned to a teacher!";
                }
            }            
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }

        public JsonResult GetTeachersByDepartmentId(int? departmentId)
        {
            var teacherList = db.Teachers.Where(a => a.DepartmentId == departmentId).ToList();
            return Json(teacherList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoursesByDepartmentId(int? departmentId)
        {
            var courseList = db.Courses.Where(a => a.DepartmentId == departmentId).ToList();
            return Json(new SelectList(courseList, "Id", "Code"));
        }

        public JsonResult GetCourseNameAndCreditByCourseId(int? courseId)
        {
            var courses = db.Courses.ToList();
            var courseInfo = courses.Where(a => a.Id == courseId).Select(a => new { a.Name, a.Credit }).ToList();
            return Json(courseInfo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeachersCreditToBeTakenAndRemainingCredit(int? teacherId)
        {
            Teacher teacherCredit = db.Teachers.Where(a => a.Id == teacherId).ToList().SingleOrDefault();
            Teacher teacherCreditInfo = new Teacher();
            if (teacherCredit != null)
            {
                teacherCreditInfo.CreditToBeTaken = teacherCredit.CreditToBeTaken;
                teacherCreditInfo.RemainingCredit = teacherCreditInfo.CreditToBeTaken - (decimal)db.Courses.Where(a => a.TeacherId == teacherId).Select(a => a.Credit).ToList().Sum();
            }
            return Json(teacherCreditInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Statistics()
        {
            var departmentList = db.Departments.ToList();
            ViewBag.Departments = departmentList;
            return View();
        }
        public JsonResult GetCourseStatistics(int? departmentId)
        {
            var getCourseByDepartmentId = db.Courses.Where(a => a.DepartmentId == departmentId).ToList();
            List<CourseStatistics> courseStatisticsList = new List<CourseStatistics>();
            foreach (var item in getCourseByDepartmentId)
            {
                CourseStatistics courseStatistics = new CourseStatistics()
                {
                    Code = item.Code,
                    Name = item.Name,
                    Semester = item.Semester.Name,
                    AssignedTo = item.TeacherId == null? "Not Assigned Yet" : item.Teacher.Name
                };
                courseStatisticsList.Add(courseStatistics);
            }
            return Json(courseStatisticsList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckIfCourseIsAlreadyAssigned(int courseId)
        {
            string message = "";
            Course getCourseById = db.Courses.Single(x => x.Id == courseId);
            if (getCourseById.TeacherId != null)
            {
                message = "This course has already been assigned to a teacher!";
            }
            else
            {
                message = "";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Unassign()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Unassign")]
        public ActionResult UnassignCourses()
        {
            if (courseManager.UnassignCourses())
            {
                ViewBag.SuccessMessage = "All Courses Unassigned Successfully!";
            }
            else
            {
                ViewBag.SuccessMessage = "All rooms are already unassigned!";
            }
            return View();
        }
    }
}
