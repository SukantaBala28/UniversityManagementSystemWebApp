using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCRMS_Version2.Models
{
    public class CourseStatistics
    {
        public int Id { get; set; }
        public string Code { set; get; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public string AssignedTo { get; set; }
    }
}