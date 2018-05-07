using System;
using System.Collections.Generic;
using Realms;
namespace RUTimetable
{
	public class VenueData:RealmObject
	{
		public DateTimeOffset DateSubmitted = DateTime.Now;
		public IList<VenueLocation> Venues { get; }
	}
}
