using System;
using System.Collections.Generic;
using System.ComponentModel;
using Realms;

namespace RUTimetable
{
	/// <summary>
	/// Student.
	/// </summary>
	public class Student : RealmObject,INotifyPropertyChanged//One to many relationship
	{
		public string StudentNo { get; set; }
		public IList<Day> WeekSemester1 { get;}
		public IList<Day> WeekSemester2 { get; }
		public IList<SubjectReminder> Reminders { get; }
	}
}
