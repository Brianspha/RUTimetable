using System;
using Realms;

namespace RUTimetable
{
	public class CustomPosition:RealmObject
	{
		public double Lat { get; set; }
		public double Long { get; set; }
		public string Name { get; set; }
	}
}
