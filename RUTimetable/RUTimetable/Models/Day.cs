using System;
using Realms;
using System.Collections;
using RUTimetable;
using System.Collections.Generic;
using System.ComponentModel;

namespace RUTimetable
{
	/// <summary>
	/// Day.
	/// </summary>
	public class Day : RealmObject,INotifyPropertyChanged
	{
		public int Semester { get; set; }
		public string day { get; set; }
		public IList<Subject> Subjects { get; }
	}
}
