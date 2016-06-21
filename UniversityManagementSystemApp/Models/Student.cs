using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace UCRMS_Version2.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Please enter a Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter an Email")]
        [EmailAddress]
        [Index(IsUnique = true)]
        [StringLength(50)]
        [Remote("IsStudentEmailExists", "Student", ErrorMessage = "Email already exists")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a Contact Number")]
        [StringLength(15)]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please select a Date")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Please enter an address")]
        [StringLength(200)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please select a Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public List<Course> Courses { get; set; }
    }
}