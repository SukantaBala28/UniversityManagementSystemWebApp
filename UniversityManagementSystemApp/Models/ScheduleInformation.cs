using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCRMS_Version2.Models
{
    public class ScheduleInformation
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string RoomName { get; set; }
        public string Day { get; set; }
        public string ClassStartFrom { get; set; }
        public string ClassEndAt { get; set; }
    }
}