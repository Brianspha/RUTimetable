using System;
using System.ComponentModel;
using Realms;
namespace RUTimetable
{
	public class SubjectReminder:RealmObject,INotifyPropertyChanged
	{
		// i didnt add the Time attribute because thats taken care of by the alarm manager for each platform
		public Subject Subject { get; set; }
		public string DayOfWeek { get; set; }
		public bool OnOf { get; set; }
		public int Semester { get; set; }
		public int ReminderID = new Random().Next(0, Int32.MaxValue);
	}
}
