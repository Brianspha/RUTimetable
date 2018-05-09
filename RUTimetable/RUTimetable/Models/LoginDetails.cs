using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTimetable.Models
{
    public class LoginDetails:RealmObject
    {
        public string ServerUrl { get; set; }

        public string Username { get; set; }
    }
}
