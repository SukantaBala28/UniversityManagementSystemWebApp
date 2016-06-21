using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UCRMS.BLL;
using UCRMS_Version2.Models;

namespace UCRMS_Version2.Controllers
{
    public class ClassroomController : Controller
    {
        UniversityDbContext db = new UniversityDbContext();
        ClassroomManager classroomManager = new ClassroomManager();

        public ActionResult ScheduleInformation()
        {
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }

        public ActionResult Allocate()
        {
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Rooms = db.Rooms.ToList();
            ViewBag.Days = GetDaysList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Allocate([Bind(Include = "Id,DepartmentId,CourseId,ClassroomId,Day,ClassStartFrom,ClassEndAt")] Classroom classroom)
        {
            if (!classroomManager.CheckForOverlapping(classroom.ClassroomId, classroom.Day, classroom.ClassStartFrom, classroom.ClassEndAt).Item1)
            {
                if (ModelState.IsValid)
                {
                    db.Classrooms.Add(classroom);
                    db.SaveChanges();
                    ViewBag.SuccessMessage = "New Schedule Saved Successfully!";
                }
            }
            else
            {
                ViewBag.FailMessage = classroomManager.CheckForOverlapping(classroom.ClassroomId, classroom.Day, classroom.ClassStartFrom,
                    classroom.ClassEndAt).Item2;
            }
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Rooms = db.Rooms.ToList();
            ViewBag.Days = GetDaysList();
            return View();
        }

        public List<Day> GetDaysList()
        {
            List<Day> daysList = new List<Day>
            {
                new Day {Id = 1, Name = "Sunday"}, new Day {Id = 2, Name = "Monday"}, new Day {Id = 3, Name = "Tuesday"},
                new Day {Id = 4, Name = "Wednesday"}, new Day {Id = 5, Name = "Thursday"}, new Day {Id = 6, Name = "Friday"},
                new Day {Id = 7, Name = "Saturday"}
            };
            return daysList;
        }

        public JsonResult GetCoursesByDepartmentId(int? departmentId)
        {
            var courseList = db.Courses.Where(a => a.DepartmentId == departmentId).ToList();
            return Json(new SelectList(courseList, "Id", "Name"));
        }

        public JsonResult GetRoomAllocationInfoList(int departmentId)
        {
            var scheduleInfoList = classroomManager.GetScheduleInformation(departmentId);
            return Json(scheduleInfoList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Unallocate()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Unallocate")]
        public ActionResult UnallocateRooms()
        {
            if (classroomManager.UnallocateRooms())
            {
                ViewBag.SuccessMessage = "All rooms unallocated Successfully!";
            }
            else
            {
                ViewBag.SuccessMessage = "All rooms are already unallocated!";
            }
            return View();
        }

    }
}
