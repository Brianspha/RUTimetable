using System;
using Realms;

namespace RUTimetable
{
	public class DirectionsRequest:RealmObject
	{
		public CustomPosition A { get; set; }
		public CustomPosition B { get; set; }
	}
}
