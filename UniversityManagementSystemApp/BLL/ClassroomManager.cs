using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCRMS_Version2.DAL;
using UCRMS_Version2.Models;

namespace UCRMS.BLL
{
    public class ClassroomManager
    {
        UniversityDbContext db = new UniversityDbContext();
        ClassroomGateway classroomGateway = new ClassroomGateway();
        public Tuple<bool, string> CheckForOverlapping(int classroomId, string day, string startTime, string endTime)
        {
            List<Classroom> classrooms = db.Classrooms.Where(c => c.ClassroomId == classroomId && c.Day == day).ToList();
            bool isConflict = false;
            string startTimeOfOverlap = "";
            string endTimeOfOverlap = "";
            string notificationMessage = "";
            string newStartMeridian = startTime.Substring(startTime.Length - 2);
            string newEndMeridian = endTime.Substring(endTime.Length - 2);
            string newStartTime = startTime.Substring(0, startTime.Length - 2);
            string newEndTime = endTime.Substring(0, endTime.Length - 2);

            if (newStartMeridian != newEndMeridian)
            {
                foreach (var classroom in classrooms)
                {
                    string oldStartMeridian = classroom.ClassStartFrom.Substring(classroom.ClassStartFrom.Length - 2);
                    string oldEndMeridian = classroom.ClassEndAt.Substring(classroom.ClassEndAt.Length - 2);


                    string oldStartTime = classroom.ClassStartFrom.Substring(0, classroom.ClassStartFrom.Length - 2);
                    string oldEndTime = classroom.ClassEndAt.Substring(0, classroom.ClassEndAt.Length - 2);


                    if (oldEndMeridian != newStartMeridian && newEndMeridian != oldStartMeridian)
                    {
                        if (oldStartMeridian == oldEndMeridian && newStartMeridian == newEndMeridian)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.ClassStartFrom;
                            endTimeOfOverlap = classroom.ClassEndAt;
                            notificationMessage = "Sorry for this room there is already an schedule of " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day. So, choose a new schedule please.";
                            break;
                        }
                    }
                    else if (newEndMeridian != oldStartMeridian && oldEndMeridian == newStartMeridian)
                    {
                        if (oldEndTime.CompareTo(newStartTime) <= 0)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.ClassStartFrom;
                            endTimeOfOverlap = classroom.ClassEndAt;
                            notificationMessage = "Sorry for this room there is already an schedule of " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day. So, choose a new schedule please.";
                            break;
                        }
                    }
                    else if (oldEndMeridian != newStartMeridian && newEndMeridian == oldStartMeridian)
                    {
                        if (newEndTime.CompareTo(oldStartTime) <= 0)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.ClassStartFrom;
                            endTimeOfOverlap = classroom.ClassEndAt;
                            notificationMessage = "Sorry for this room there is already an schedule of " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day. So, choose a new schedule please.";
                            break;
                        }
                    }
                    else
                    {
                        if (classroom.ClassEndAt.CompareTo(startTime) <= 0 ||
                            endTime.CompareTo(classroom.ClassStartFrom) <= 0)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.ClassStartFrom;
                            endTimeOfOverlap = classroom.ClassEndAt;
                            notificationMessage = "Sorry for this room there is already an schedule of " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day. So, choose a new schedule please.";
                            break;
                        }
                    }
                }
            }
            else if (newStartTime.CompareTo(newEndTime) == 0)
            {
                isConflict = true;
                notificationMessage = "Check the schedule entry again," + startTime + " to " + endTime +
                                      " is not a valid schedule.";
            }
            else if (newStartTime.CompareTo(newEndTime) < 1)
            {
                foreach (var classroom in classrooms)
                {
                    string oldStartMeridian = classroom.ClassStartFrom.Substring(classroom.ClassStartFrom.Length - 2);
                    string oldEndMeridian = classroom.ClassEndAt.Substring(classroom.ClassEndAt.Length - 2);


                    string oldStartTime = classroom.ClassStartFrom.Substring(0, classroom.ClassStartFrom.Length - 2);
                    string oldEndTime = classroom.ClassEndAt.Substring(0, classroom.ClassEndAt.Length - 2);


                    if (oldEndMeridian != newStartMeridian && newEndMeridian != oldStartMeridian)
                    {
                        if (oldStartMeridian == oldEndMeridian && newStartMeridian == newEndMeridian)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.ClassStartFrom;
                            endTimeOfOverlap = classroom.ClassEndAt;
                            notificationMessage = "Sorry for this room there is already an schedule of " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day. So, choose a new schedule please.";
                            break;
                        }
                    }
                    else if (newEndMeridian != oldStartMeridian && oldEndMeridian == newStartMeridian)
                    {
                        if (oldEndTime.CompareTo(newStartTime) <= 0)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.ClassStartFrom;
                            endTimeOfOverlap = classroom.ClassEndAt;
                            notificationMessage = "Sorry for this room there is already an schedule of " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day. So, choose a new schedule please.";
                            break;
                        }
                    }
                    else if (oldEndMeridian != newStartMeridian && newEndMeridian == oldStartMeridian)
                    {
                        if (newEndTime.CompareTo(oldStartTime) <= 0)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.ClassStartFrom;
                            endTimeOfOverlap = classroom.ClassEndAt;
                            notificationMessage = "Sorry for this room there is already an schedule of " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day. So, choose a new schedule please.";
                            break;
                        }
                    }
                    else
                    {
                        if (classroom.ClassEndAt.CompareTo(startTime) <= 0 ||
                            endTime.CompareTo(classroom.ClassStartFrom) <= 0)
                        {
                            isConflict = false;
                        }
                        else
                        {
                            isConflict = true;
                            startTimeOfOverlap = classroom.ClassStartFrom;
                            endTimeOfOverlap = classroom.ClassEndAt;
                            notificationMessage = "Sorry for this room there is already an schedule of " +
                                                  startTimeOfOverlap +
                                                  " to " + endTimeOfOverlap +
                                                  " on this day. So, choose a new schedule please.";
                            break;
                        }
                    }
                }
            }
            else
            {
                isConflict = true;
                notificationMessage = "Check the schedule entry again," + startTime + " to " + endTime +
                                      " is not a valid schedule.";
            }

            return Tuple.Create<bool, string>(isConflict, notificationMessage);
        }
        public List<ScheduleInformation> GetScheduleInformation(int departmentId)
        {
            return classroomGateway.GetScheduleInformation(departmentId);
        }
        public bool UnallocateRooms()
        {
            return classroomGateway.UnallocateRooms();
        }
    }
}