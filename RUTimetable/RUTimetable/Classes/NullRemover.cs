using System;
using System.Collections.Generic;
using System.Linq;

namespace RUTimetable
{
	public class NullRemover
	{
		List<Day> Subjects;
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Timetable.NullRemover"/> class.
		/// </summary>
		/// <param name="obj">Object.</param>
		public NullRemover(List<Day> obj)
		{
			Subjects = obj;//reason for not checking if the Obj list is empty a student may have no lecture for a particular day
		}
		 Dictionary<int, string> RemoveNullValues(Dictionary<int, string> m)
		{
			Dictionary<int, string> temp = new Dictionary<int, string>();
			for (int i = 0; i < m.Count; i++)
			{
				if (m.Values.ToList()[i] != null && m.Values.ToList()[i] != "")
				{
					temp.Add(m.Keys.ToList()[i], m.Values.ToList()[i]);
				}
			}
			return temp;
		}
		/// <summary>
		/// Adds the subjects.
		/// </summary>
		/// <returns>The subjects.</returns>
		/*public Dictionary<int, string> AddSubjects()
		{
			Dictionary<int, string> SubjectsInDayForCardView = new Dictionary<int, string>();
			if (Subjects.Count > 0)
			{
				SubjectsInDayForCardView.Add(1, Subjects[0].Periodone);
				SubjectsInDayForCardView.Add(2, Subjects[0].Periodtwo);
				SubjectsInDayForCardView.Add(3, Subjects[0].Periodthree);
				SubjectsInDayForCardView.Add(4, Subjects[0].Periodfour);
				SubjectsInDayForCardView.Add(5, Subjects[0].Periodfive);
				SubjectsInDayForCardView.Add(6, Subjects[0].Periodsix);
				SubjectsInDayForCardView.Add(7, Subjects[0].Periodseven);
				SubjectsInDayForCardView.Add(8, Subjects[0].Periodeight);
				SubjectsInDayForCardView.Add(9, Subjects[0].Periodnine);
				SubjectsInDayForCardView.Add(10, Subjects[0].Periodten);
				SubjectsInDayForCardView = RemoveNullValues(SubjectsInDayForCardView);//remove all null values
			}
			return SubjectsInDayForCardView;
		}*/

	}
}
