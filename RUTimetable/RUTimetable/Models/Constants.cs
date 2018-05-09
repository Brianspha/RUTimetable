using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTimetable.Models
{
   public static class Constants
   {
        public const string ServerInstance = "rhodesmap.us1.cloud.realm.io";
        public const string AUTH_URL = "https://" + ServerInstance +"/auth";
        public const string UserRole = "Admin";
        public static string SyncHost { get; set; } = "127.0.0.1:9080";


    }
}
