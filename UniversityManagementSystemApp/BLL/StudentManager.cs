using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UCRMS_Version2.Models;

namespace UCRMS_Version2.BLL
{
    public class StudentManager
    {
        UniversityDbContext db = new UniversityDbContext();
        public string GenerateRegistrationNumber(int departmentId, string regDate)
        {
            string rollNumber = "";
            var departmentCode = db.Departments.Where(a => a.Id == departmentId).Select(a => a.Code).Single();
            int registraionCount = db.Students.Where(a => a.DepartmentId == departmentId && a.Date.Contains(regDate.Substring(regDate.Length - 4))).ToList().Count;

            if (registraionCount < 10)
            {
                registraionCount = registraionCount == 0 ? 1 : (registraionCount+1);
                rollNumber = "00" + registraionCount;
            }
            else
            {
                rollNumber = registraionCount < 100 ? "0" + (registraionCount+1) : (registraionCount+1).ToString();
            }

            return departmentCode + "-" + regDate.Substring(regDate.Length - 4) + "-" + rollNumber;
        }
    }
}