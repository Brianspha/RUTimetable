using System;
using System.ComponentModel;
using Realms;

namespace RUTimetable
{
	public class Subject : RealmObject, INotifyPropertyChanged
	{
		public string Name { get; set; }
		public string Period { get; set; }
		public string Time { get; set; }
		public int Semester { get; set; }
		public bool IsReminderSet { get; set; }
		public string FallsIn { get; set; }
		public bool IsPrac { get; set; }
		public VenueLocation Venue {get;set;}

	}
}
