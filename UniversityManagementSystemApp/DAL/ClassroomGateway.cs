using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UCRMS_Version2.Models;

namespace UCRMS_Version2.DAL
{
    public class ClassroomGateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["UniversityDbContext"].ConnectionString;

        private SqlConnection connection;

        public ClassroomGateway()
        {
            connection = new SqlConnection(connectionString);
        }
        public List<ScheduleInformation> GetScheduleInformation(int departmentId)
        {
            List<ScheduleInformation> roomAllocationInfoList = new List<ScheduleInformation>();
            string query = "SELECT * FROM ScheduleInformationView WHERE DepartmentId='" + departmentId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ScheduleInformation roomAllocationInfo = new ScheduleInformation()
                {
                    Code = reader["Code"].ToString(),
                    Name = reader["Name"].ToString(),
                    Day = reader["Day"].ToString() != ""? reader["Day"].ToString().Substring(0, 3): reader["Day"].ToString(),
                    RoomName = reader["RoomName"].ToString(),
                    ClassStartFrom = reader["ClassStartFrom"].ToString(),
                    ClassEndAt = reader["ClassEndAt"].ToString()
                };
                roomAllocationInfoList.Add(roomAllocationInfo);
            }
            connection.Close();
            return roomAllocationInfoList;
        }

        public bool UnallocateRooms()
        {
            string query = "DELETE FROM Classrooms";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            bool isUnallocated = rowsAffected > 0;
            return isUnallocated;
        }
    }
}