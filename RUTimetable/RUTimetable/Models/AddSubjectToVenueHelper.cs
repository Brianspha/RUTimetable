using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
namespace RUTimetable.Models
{
   public class AddSubjectToVenueHelper:RealmObject
    {
        public string Venue { get; set; }
        public string NewVenue { get; set; }
        public string Subject { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
