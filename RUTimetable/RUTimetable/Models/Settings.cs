using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RUTimetable.Models
{
    /// <summary>
    ///Used for settings for the App
    /// </summary>
    public class Settings:RealmObject
    {
        public int Seconds { get; set; } 
    }
}
