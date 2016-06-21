using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Ajax.Utilities;
using UCRMS_Version2.BLL;
using UCRMS_Version2.Models;

namespace UCRMS_Version2.Controllers
{
    public class StudentController : Controller
    {
        private UniversityDbContext db = new UniversityDbContext();
        StudentManager manager = new StudentManager();

        public ActionResult Register()
        {
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,RegistrationNumber,Name,Email,ContactNo,Date,Address,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                ViewBag.SuccessMessage = "New Student Registered Successfully!";
            }
            ViewBag.Departments = db.Departments.ToList();
            return View();
        }

        public JsonResult IsStudentEmailExists(string email)
        {
            return Json(!db.Students.Any(x => x.Email == email), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRegistrationNo(int departmentId, string regDate)
        {
            var regNo = manager.GenerateRegistrationNumber(departmentId, regDate);
            return Json(regNo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EnrollCourse()
        {
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegistrationNumber");
            return View();
        }

        [HttpPost]
        public ActionResult EnrollCourse(int? studentId, int? courseId, string date)
        {
            if (studentId != null && courseId != null && date != null)
            {
                var studentIds = db.Database.SqlQuery<int>(
                      "SELECT Student_Id FROM dbo.StudentCourses Where Student_Id =" + studentId + " AND Course_Id = " + courseId).ToList();

                if (studentIds.Count == 0)
                {
                    if (ModelState.IsValid)
                    {
                        db.Database.ExecuteSqlCommand(
                           "INSERT INTO dbo.StudentCourses(Student_Id,Course_Id,EnrollDate) VALUES('" + studentId + "','" + courseId + "','" + date + "')");
                        ViewBag.SuccessMessage = "Student enrolled into department successfully!";
                    }
                }
                else
                {
                    ViewBag.FailMessage = "Student has already enrolled in this course.";
                }
            }
            else
            {
                ViewBag.FailMessage = "Please fill all field with proper data.";
            }

            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegistrationNumber");
            return View();
        }

        public JsonResult GetStudentInfoByStudentId(int studentId)
        {
            var studentList = db.Students.Where(s => s.Id == studentId).ToList();

            return Json(studentList.Select(c => new
            {
                name = c.Name,
                email = c.Email,
                department = db.Departments.Where(x => x.Id == c.DepartmentId).Select(x => x.Name).Single()
            }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCoursesByStudentId(int studentId)
        {
            var student = db.Students.Find(studentId);
            var courses = db.Courses.Where(c => c.DepartmentId == student.DepartmentId);
            return Json(new SelectList(courses, "Id", "Name"));
        }

        public ActionResult SaveResult()
        {
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegistrationNumber");
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult SaveResult(int? studentId, int? courseId, int? gradeId)
        {
            if (studentId != null && courseId != null && gradeId != null)
            {
                var studentIds = db.Database.SqlQuery<int>(
                     "SELECT Student_Id FROM dbo.StudentCourses Where Student_Id =" + studentId + " AND Course_Id = " + courseId).ToList();

                var count = studentIds.Count;
                if (count == 1)
                {
                    if (ModelState.IsValid)
                    {
                        db.Database.ExecuteSqlCommand(
                           "UPDATE dbo.StudentCourses SET Grade = '" + gradeId + "' WHERE Student_Id = '" + studentId + "' AND Course_Id = '" + courseId + "'");
                        ViewBag.SuccessMessage = " Student result saved Successfully";
                    }
                }
                else
                {
                    ViewBag.Message = "Please enroll the course first.";
                    //ata bad jabe
                }
            }
            else
            {
                ViewBag.Message = "Please fill all field with proper data.";
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegistrationNumber");
            ViewBag.GradeId = new SelectList(db.Grades, "Id", "Name");
            return View();

        }

        public JsonResult GetEnrollCoursesByStudentId(int studentId)
        {
            var courseIds = db.Database.SqlQuery<int>(
                      "SELECT Course_Id FROM dbo.StudentCourses Where Student_Id =" + studentId).ToList();
            var courseList = new List<Course>();

            foreach (var courseId in courseIds)
            {
                var course = db.Courses.SingleOrDefault(c => c.Id == courseId);
                courseList.Add(course);
            }
            var courses = courseList.AsQueryable();
            return Json(new SelectList(courses, "Id", "Name"));

        }

        public ActionResult ViewResult()
        {
            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegistrationNumber");
            return View();
        }

        [HttpPost]
        public ActionResult ViewResult(int? studentId)
        {
            if (studentId != null)
            {
                var getStudentById = db.Students.SingleOrDefault(s => s.Id == studentId);

                //Pdf part start
                // Create a Document object
                var document = new Document(PageSize.A4, 50, 50, 25, 25);

                // Create a new PdfWriter object, specifying the output stream
                var output = new MemoryStream();
                var writer = PdfWriter.GetInstance(document, output);

                // Open the Document for writing
                document.Open();


                var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
                var subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
                var boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
                var endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
                var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);

                document.Add(new Paragraph("UCRMS Result Creator", titleFont));
                document.Add(new Paragraph("Thank you for using our app for result creating.", bodyFont));
                document.Add(new Paragraph("Student Results are below", bodyFont));

                document.Add(Chunk.NEWLINE);

                document.Add(new Paragraph("Student Information: ", subTitleFont));

                var studentInfoTable = new PdfPTable(2);
                studentInfoTable.HorizontalAlignment = 0;
                studentInfoTable.SpacingBefore = 10;
                studentInfoTable.SpacingAfter = 10;
                studentInfoTable.DefaultCell.Border = 0;
                studentInfoTable.SetWidths(new int[] { 1, 4 });

                studentInfoTable.AddCell(new Phrase("Student Reg. No.:", boldTableFont));
                studentInfoTable.AddCell(getStudentById.RegistrationNumber);
                studentInfoTable.AddCell(new Phrase("Name :", boldTableFont));
                studentInfoTable.AddCell(getStudentById.Name);
                studentInfoTable.AddCell(new Phrase("Email :", boldTableFont));
                studentInfoTable.AddCell(getStudentById.Email);
                studentInfoTable.AddCell(new Phrase("Department :", boldTableFont));
                studentInfoTable.AddCell(db.Departments.Where( c => c.Id == getStudentById.DepartmentId).Select(x => x.Name).Single());

                document.Add(studentInfoTable);

                document.Add(Chunk.NEWLINE);

                document.Add(new Paragraph("Result Informatiion: ", subTitleFont));

                var resultTable = new PdfPTable(3);
                resultTable.HorizontalAlignment = 1;
                resultTable.SpacingBefore = 10;
                resultTable.SpacingAfter = 10;
                //resultTable.DefaultCell.Border = 1;
                resultTable.TotalWidth = 9f;

                resultTable.AddCell(new Phrase("Course Code", boldTableFont));
                resultTable.AddCell(new Phrase("Name", boldTableFont));
                resultTable.AddCell(new Phrase("Grade", boldTableFont));

                //result asbe
                var courseIds = db.Database.SqlQuery<int>(
                    "SELECT Course_Id FROM dbo.StudentCourses Where Student_Id =" + studentId).ToList();
                var gradeIds = db.Database.SqlQuery<int?>(
                    "SELECT Grade FROM dbo.StudentCourses Where Student_Id =" + studentId).ToList();

                List<CourseGrade> cList = new List<CourseGrade>();

                var count = courseIds.Count;

                for (int i = 0; i < count; i++)
                {
                    var value = gradeIds[i];
                    var values = courseIds[i];
                    var course = db.Courses.SingleOrDefault(c => c.Id == values);
                    var grade = db.Grades.SingleOrDefault(g => g.Id == value);
                    CourseGrade cGrade = new CourseGrade();
                    cGrade.CourseId = values;
                    if (value == null)
                    {
                        cGrade.GradeId = 0;
                        cGrade.GradeName = "Not Graded Yet";
                    }
                    else
                    {
                        cGrade.GradeId = (int)value;
                        cGrade.GradeName = grade.Name;
                    }

                    cGrade.CourseCode = course.Code;
                    cGrade.CourseName = course.Name;

                    resultTable.AddCell(cGrade.CourseCode);
                    resultTable.AddCell(cGrade.CourseName);
                    resultTable.AddCell(cGrade.GradeName);


                    cList.Add(cGrade);
                }

                //result asbe

                //resultTable.AddCell();
                document.Add(resultTable);


                DateTime dateTime = DateTime.Now;

                PdfContentByte cb = writer.DirectContent;
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.SetFontAndSize(bf, 10);
                cb.BeginText();
                cb.SetTextMatrix(50, 5);
                cb.ShowText(dateTime.ToString());
                cb.EndText();
                document.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition",
                    string.Format("attachment;filename=Receipt-{0}.pdf", getStudentById.RegistrationNumber));
                Response.BinaryWrite(output.ToArray());
                ViewBag.Message = dateTime.ToString();
                ////pdf ends here
            }
            else
            {
                ViewBag.Message = "Fill all field properly";
            }

            ViewBag.StudentId = new SelectList(db.Students, "Id", "RegistrationNumber");
            return View();
        }

        public JsonResult GetCourseResultInfoByStudentId(int studentId)
        {
            var courseIds = db.Database.SqlQuery<int>(
                        "SELECT Course_Id FROM dbo.StudentCourses Where Student_Id =" + studentId).ToList();
            var gradeIds = db.Database.SqlQuery<int?>(
                    "SELECT Grade FROM dbo.StudentCourses Where Student_Id =" + studentId).ToList();

            List<CourseGrade> cList = new List<CourseGrade>();

            var count = courseIds.Count;

            for (int i = 0; i < count; i++)
            {
                var value = gradeIds[i];
                var values = courseIds[i];
                var course = db.Courses.SingleOrDefault(c => c.Id == values);
                var grade = db.Grades.SingleOrDefault(g => g.Id == value);
                CourseGrade cGrade = new CourseGrade();
                cGrade.CourseId = values;
                if (value == null)
                {
                    cGrade.GradeId = 0;
                    cGrade.GradeName = "Not Graded Yet";
                }
                else
                {
                    cGrade.GradeId = (int)value;
                    cGrade.GradeName = grade.Name;
                }

                cGrade.CourseCode = course.Code;
                cGrade.CourseName = course.Name;

                cList.Add(cGrade);
            }

            return Json(cList.Select(x => new
            {
                code = x.CourseCode,
                name = x.CourseName,
                grade = x.GradeName


            }));

        }
    }
}
