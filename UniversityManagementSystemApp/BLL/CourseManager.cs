using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCRMS_Version2.DAL;

namespace UCRMS_Version2.BLL
{
    public class CourseManager
    {
        CourseGateway courseGateway = new CourseGateway();
        public bool UnassignCourses()
        {
            return courseGateway.UnassignCourses();
        }
    }
}