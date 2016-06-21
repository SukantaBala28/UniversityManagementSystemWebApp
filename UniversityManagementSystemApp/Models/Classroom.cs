using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UCRMS_Version2.Models
{
    public class Classroom
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select a Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please select a Course")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Please select a Room No.")]
        public int ClassroomId { get; set; }
        [Required(ErrorMessage = "Please select a Day")]
        public string Day { get; set; }
        [Required(ErrorMessage = "Please enter class starting time")]
        public string ClassStartFrom { get; set; }
        [Required(ErrorMessage = "Please enter when class will end")]
        public string ClassEndAt { get; set; }
        public virtual Department Department { get; set; }
        public virtual Course Course { get; set; }
        public virtual Room Room { get; set; }

    }
}