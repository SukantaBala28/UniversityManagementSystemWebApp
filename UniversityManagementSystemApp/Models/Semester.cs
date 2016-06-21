using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UCRMS_Version2.Models
{
    public class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Course> Courses { get; set; }
    }
}