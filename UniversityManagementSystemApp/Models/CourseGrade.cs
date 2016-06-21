using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCRMS_Version2.Models
{
    public class CourseGrade
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public int GradeId { get; set; }
        public string GradeName { get; set; }
    }
}