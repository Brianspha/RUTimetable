using System;
namespace RUTimetable
{
	public class SemesterChangeHandler
	{
		RealmDataBase db;
		public SemesterChangeHandler()
		{
			db = new RealmDataBase();
		}
		/// <summary>
		/// Communicates the with other tabs.
		/// How?by storing the semester which the user clicked on ,on the ToolbarMenuItems
		/// </summary>
		public void CommunicateWithOtherTabs(int semester)
		{
			db.SemesterToLoad(semester);
		}
		public Semester GetSemester()
		{
			return db.GetSemesterToLoad();
		}
	}
}
