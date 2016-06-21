using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCRMS_Version2.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Name of the Teacher")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter Address of the Teacher")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter Email Address of the Teacher")]
        [EmailAddress]
        [Index(IsUnique = true)]
        [Remote("IsTeacherEmailExists", "Teacher", ErrorMessage = "Email already exists! Please try another.")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter Contact No. of the Teacher")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please enter the Credit to be taken by the Teacher")]
        [Range(0, Double.MaxValue, ErrorMessage = "Credit to be taken must be a non-negative value")]
        public decimal CreditToBeTaken { get; set; }
        [NotMapped]
        public decimal RemainingCredit { get; set; }
        [Required(ErrorMessage = "Please select Designation of the Teacher")]
        public int DesignationId { get; set; }
        [Required(ErrorMessage = "Please select Department for the Teacher")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }
    }
}