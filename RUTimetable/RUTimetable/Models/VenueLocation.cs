using System;
using System.Collections.Generic;
using Realms;
namespace RUTimetable
{
	public class VenueLocation : RealmObject
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public double Lat { get; set; }
		public double Long { get; set; }
		public IList<Subject> VenueSubjects {get;}
		public DateTimeOffset LastUpdated = DateTime.Now;
    }
}
