using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UCRMS_Version2.DAL
{
    public class CourseGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDbContext"].ConnectionString;

        private SqlConnection connection;

        public CourseGateway()
        {
            connection = new SqlConnection(connectionString);
        }

        public bool UnassignCourses()
        {
            string query = "UPDATE Courses SET TeacherId=NULL";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            bool isUnassigned = rowsAffected > 0;
            return isUnassigned;
        }

    }
}