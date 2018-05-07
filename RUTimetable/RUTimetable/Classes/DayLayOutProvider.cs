using System;
using System.Collections.Generic;
using System.Linq;
using Realms;
using Timetable.Exception;

namespace RUTimetable
{
	public class DayLayOutProvider
	{
		Realm db;
		public DayLayOutProvider()
		{
			db = Realm.GetInstance(); //Get default realm database for App
		}

		/// <summary>
		/// Sets up day.
		/// </summary>
		/// <returns>The up day.</returns>
		/// <param name="day">Day.</param>
		/// <param name="subjects">Subjects.</param>
		/// 
		Day SetUpDay(Day day, List<string> subjects)
		{
			var temp = day;
			try
			{
				temp.Semester = Convert.ToInt32(subjects[1]);
				temp.day = subjects[subjects.Count - 1] != "" ? subjects[subjects.Count - 1] : "";

			}
			catch (TimeTableException)
			{
				return temp;
			}
			return temp;
		}
		/// <summary>
		/// Gets the days.
		/// </summary>
		/// <returns>The days.</returns>
		/// <param name="Sno">Sno refers to a specific student Number.</param>
		public List<Day> GetDay(string day, string Semester)
		{
			//Linq doesnt support requering results from a linq result set so i have to itterate through the data myself
			List<Day> temp = new List<Day>();
			List<Day> temp2 = new List<Day>();
			if (db.All<Student>().Count() > 0)
			{
				var test = db.All<Student>().ElementAt(0);
				if (Semester == "1")
				{
					temp = test.WeekSemester1.ToList();
				}
				else temp = test.WeekSemester2.ToList();
				foreach (Day d in temp)
				{
					if (d.Semester == Convert.ToInt32(Semester) && d.day == day)
					{
						temp2.Add(d);
						break;//bug just add one
					}
				}
			}
			return temp2;
		}
	}
}
