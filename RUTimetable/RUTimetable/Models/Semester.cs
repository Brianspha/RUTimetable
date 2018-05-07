using System;
using Realms;

namespace RUTimetable
{
	public class Semester:RealmObject
	{
		public int semester { get; set; }

		public void Reset()
		{
			semester=0;
		}
	}
}
