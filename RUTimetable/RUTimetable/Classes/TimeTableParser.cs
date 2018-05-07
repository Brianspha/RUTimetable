using System;
using System.Collections.Generic;
using System.Linq;
using Timetable.Exception;
using RUTimetable;
using System.Globalization;
using Timetable;
namespace RUTimetable
{
	/// <summary>
	/// Time table parser.
	/// </summary>
	public class TimeTableParser
	{
		Dictionary<string, List<Dictionary<int, string>>> Semester1;
		Dictionary<string, List<Dictionary<int, string>>> Semester2;
		Dictionary<string, List<Subject>> Semester1Updated;
		Dictionary<string, List<Subject>> Semester2Updated;
		public TimeTableParser(string table)
		{
			//Todo check if student inputs surname only as this is not allowed it is but will be a headache to handle so i will dissallow it.
			if (string.IsNullOrEmpty(table))throw new TimeTableException("No Time table found for given student No/Name");
			Semester1 = new Dictionary<string, List<Dictionary<int, string>>>();
			Semester2 = new Dictionary<string, List<Dictionary<int, string>>>();
			Semester1Updated = new Dictionary<string, List<Subject>>();
			Semester2Updated = new Dictionary<string, List<Subject>>();
			ParseTable(table);
			UpdateSemesterDataStructure();
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RUTimetable.TimeTableParser"/> class.
		/// </summary>
		public TimeTableParser() { 
		
		
		}
		/// <summary>
		/// Parses the Html tablet Returned by the timetable website.
		/// </summary>
		/// <param name="g">The g represemts the table returned by the timetable website.</param>
		void ParseTable(string g)
		{

			List<string> split = g.Split('\n').ToList();
			int Periodcounter = 1;//this variable is responsible for keeping track of the index of the subjecst added
			int count = 1;//this is used to keeptrack f the periods i.2. 1 2 3 4 5 6 7 8 9 this is because of the way the data given back by the website
			for (int i = 0; i < split.Count; i++)//itterate through the HTML Table
			{
				string CurrentStringInTable = split[i];//get the current string we looking at
				if (CurrentStringInTable == "Period\r")//From how the html table is formated as soon as we see Period it means that we are currently looking at the days of the week 
				{
					Dictionary<int, string> temp = new Dictionary<int, string>();
					for (int k = i + 1; k <= i + 5; k++)//add monday to friday to the dictionary
					{
						if (!Semester1.Keys.Contains(split[k]))
						{
							Semester1.Add(split[k], new List<Dictionary<int, string>>());
							Semester2.Add(split[k], new List<Dictionary<int, string>>());
						}
					}
				}
				if (CurrentStringInTable == count + "\r")//check to see if we are looking at 
				{
					int Currentperiod = Convert.ToInt32(CurrentStringInTable);//this keeps track of which period we are currently looking at
					for (int j = i + 1; Periodcounter <= 10; j++)
					{
						string Subject = split[j] + "\r";//this could eithier be a valid subject or invalid data like a new line
						if (Subject != "&nbsp" && Subject != "&nbsp\r\r")
						{
							if (Periodcounter > 5)//if we have reached the second semester the table lists the subjects from 1 to 10 if we pass 5 it means we looking at semester 2 subjects
							{
								Dictionary<int, string> tempDictionary = new Dictionary<int, string>();
								tempDictionary.Add(Currentperiod, Subject);
								Semester2[new DayConverter(Periodcounter).ConvertToDay()].Add(tempDictionary);
								Periodcounter++;
								i = j;
							}
							else
							{
								Dictionary<int, string> tempDictionary = new Dictionary<int, string>();
								tempDictionary.Add(Currentperiod, Subject);
								Semester1[new DayConverter(Periodcounter).ConvertToDay()].Add(tempDictionary);
								Periodcounter++;
							}
						}
						else Periodcounter++;
					}
					count++;
					Periodcounter = 1;//reset daycounter back to monday
				}
			}
		}
		/// <summary>
		/// Updates the semester data structure.
		/// </summary>
		public void UpdateSemesterDataStructure()
		{

           var DayList = new Dictionary<string, List<Subject>>();
           int index = 0;
           var Times = new TimeProvider().TimeForLectures();
			foreach (var s in Semester1.Values)
			{
		     	var tempList = new List<Subject>();
				foreach (Dictionary<int, string> dets in s)
				{
				   var sub = new Subject();
                   sub.Name = dets.Values.ToList()[0];
					sub.Time = Times[dets.Keys.ToList()[0] - 1];
					sub.Period = "Period: "+dets.Keys.ToList()[0];
					sub.Semester = 1;
					tempList.Add(sub);
				}
					DayList.Add(new DayConverter().ConvertDayToString(index++),tempList);
							
			}
			Semester1Updated = DayList;
			DayList = new Dictionary<string, List<Subject>>();
			index = 0;
			foreach (var s in Semester2.Values)
			{
		     	var tempList = new List<Subject>();
				foreach (Dictionary<int, string> dets in s)
				{
				   var sub = new Subject();
					sub.Name = dets.Values.ToList()[0];
					sub.Time = Times[dets.Keys.ToList()[0] - 1];
					sub.Period = "Period: "+dets.Keys.ToList()[0];
					sub.Semester = 2;
					tempList.Add(sub);
				}
					DayList.Add(new DayConverter().ConvertDayToString(index++),tempList);							
			}
			Semester2Updated = DayList;
		}
		/// <summary>
		/// Gets the semester.
		/// </summary>
		/// <returns>The semester.</returns>
		/// <param name="WhichSemester">Which semester.</param>
		public Dictionary<string,List<Subject>> GetSemester(int WhichSemester)
		{
			
			if (WhichSemester == 1)return Semester1Updated;
			else if (WhichSemester == 2) return Semester2Updated;
			return null;
		}
		public List<string> GetAcronyms() {
			var myList = new List<string>();
			RealmDataBase db = new RealmDataBase();
			var subs = db.GetStudentSubjectLists();
			foreach (List<Day> daySubs in subs)
			{
				foreach (Day specificDay in daySubs)
				{
					foreach (Subject sub in specificDay.Subjects)
					{
						var temp = sub.Name.Split(' ');
						var accr = temp[0] + ' ' + temp[1];
						if(!myList.Contains(accr) && !sub.Name.Contains("PRACTICAL")&& !sub.Name.Contains("TUTORIAL"))myList.Add(accr);
					}
				}
			}
			return myList;		
		}
	}
}
