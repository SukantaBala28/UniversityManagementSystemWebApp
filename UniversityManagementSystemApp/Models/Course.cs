using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCRMS_Version2.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [StringLength(10, MinimumLength = 5, ErrorMessage = "Code must be at least five characters long!")]
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Please enter a Course Code")]
        [Remote("IsCourseCodeExists", "Course", ErrorMessage = "Course Code already exists. Please try another!")]
        public string Code { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Please enter a Course Name")]
        [StringLength(50)]
        [Remote("IsCourseNameExists", "Course", ErrorMessage = "Course Name already exists. Please try another!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the Course Credit"), Range(0.5, 5.0, ErrorMessage = "Credit must be between 0.5 to 5.0")]
        public double Credit { get; set; }

        [Required(ErrorMessage = "Please enter a brief description of the Course")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please select a Semester")]
        public int SemesterId { get; set; }
        public int? TeacherId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual List<Classroom> Classrooms { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}