using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCRMS_Version2.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Code must be two to seven characters!")]
        [Index(IsUnique = true)]
        [Remote("IsDepartmentCodeExists", "Department",
            ErrorMessage = "Department Code already exists. Please try another!")]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        [Remote("IsDepartmentNameExists", "Department",
            ErrorMessage = "Department Name already exists. Please try another!")]
        public string Name { get; set; }

        public virtual List<Course> Courses { get; set; }
        public virtual List<Teacher> Teachers { get; set; }
        public virtual List<Student> Students { get; set; }
        public virtual List<Classroom> Classrooms { get; set; } 

    }
}