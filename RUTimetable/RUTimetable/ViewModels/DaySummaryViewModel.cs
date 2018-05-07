using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
namespace RUTimetable
{
	public class DaySummaryViewModel : INotifyPropertyChanged
	{
        bool AnyChange = false;
		RealmDataBase db;
		public ObservableCollection<Subject> summary;
		public ObservableCollection<Subject> Summary
		{
			get { return summary; }
			set
			{
				summary = value;
				OnPropertyChanged(nameof(Summary));
			}
		}
		public DaySummaryViewModel()
		{
			db = new RealmDataBase();
			summary = new ObservableCollection<Subject>();
			Summary = new ObservableCollection<Subject>();
		}
        public bool AnyChangeYet()
        {
            return AnyChange;
        }
        /// <summary>
        /// Gets the Current days subject period that are upcoming
        /// This feature is still under development not really it works 
        ///A demo version is enabled but still needs improving 
        /// </summary>

        public void GetSummary()
        {
            summary.Clear();
            Summary.Clear();
            AnyChange = false;
            if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                var subs = db.GetDay(DateTime.Now.DayOfWeek.ToString(), DateTime.Now.Month > 6 ? 2 : 1);
                if (subs == null) return;
                var dayT2 = subs.Subjects.ToList();
                for (int i = 0; i < dayT2.Count; i++)
                {
                    var time = new TimeProvider().ConvertLectureTimeToAlarmTime(dayT2[i].Time).Split(':');
                    var DT = new DateTime(DateTime.Now.Year, DateTime.UtcNow.Month, DateTime.Now.Day, int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
                    if (DT.TimeOfDay <= DateTime.Now.TimeOfDay)
                    {
                        AnyChange = true;
                        if (dayT2.Count > 1)
                        {
                            for (int i2 = 0; i2 < dayT2.Count; i2++)// this is to check if we already had the lectures listed in the day summary or not
                            {
								time = new TimeProvider().ConvertLectureTimeToAlarmTime(dayT2[i2].Time).Split(':');
							    DT = new DateTime(DateTime.Now.Year, DateTime.UtcNow.Month, DateTime.Now.Day, int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
                                if (DT.TimeOfDay <= DateTime.Now.TimeOfDay)
                                {
									//for some odd reason removing an item off the list doesnt work
									var removed =dayT2.Remove(dayT2[i2]);
									var k = "";
                                }
                            }
							if (dayT2.Count > 0)
							{
								time = new TimeProvider().ConvertLectureTimeToAlarmTime(dayT2[0].Time).Split(':');
								DT = new DateTime(DateTime.Now.Year, DateTime.UtcNow.Month, DateTime.Now.Day, int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
								if (DT.TimeOfDay <= DateTime.Now.TimeOfDay)
								{
									Summary.Add(new Subject { Name = dayT2[i].Name, Period = dayT2[i].Period, Venue = dayT2[i].Venue, Time = dayT2[i].Time, Semester = dayT2[i].Semester, IsReminderSet = dayT2[i].IsReminderSet, FallsIn = "Passed Lecture" });
								}
							}
							if (i + 1 < dayT2.Count)
                            {
                                Summary.Add(new Subject { Name = dayT2[i + 1].Name, Period = dayT2[i + 1].Period, Venue = dayT2[i + 1].Venue, Time = dayT2[i + 1].Time, Semester = dayT2[i + 1].Semester, IsReminderSet = dayT2[i + 1].IsReminderSet, FallsIn = "Up-Comming " });
                            }
                            if (i + 2 < dayT2.Count)
                            {
                                Summary.Add(new Subject { Name = dayT2[i + 2].Name, Period = dayT2[i + 2].Period, Venue = dayT2[i + 2].Venue, Time = dayT2[i + 2].Time, Semester = dayT2[i + 2].Semester, IsReminderSet = dayT2[i + 2].IsReminderSet, FallsIn = "Later on" });
                            }
                            break;
                        }
                    }
                    if (DT.TimeOfDay >= DateTime.Now.TimeOfDay && DateTime.Now.TimeOfDay > new TimeSpan(6, 01, 01) && DateTime.Now.ToString("tt") != "PM")
                    {
                        AnyChange = true;
                        Summary.Add(new Subject { Name = dayT2[i].Name, Period = dayT2[i].Period, Venue = dayT2[i].Venue, Time = dayT2[i].Time, Semester = dayT2[i].Semester, IsReminderSet = dayT2[i].IsReminderSet, FallsIn = "1st Lecture/Practical/Tutorial" });
                        if (i + 1 < dayT2.Count)
                        {
                            Summary.Add(new Subject { Name = dayT2[i + 1].Name, Period = dayT2[i + 1].Period, Venue = dayT2[i + 1].Venue, Time = dayT2[i + 1].Time, Semester = dayT2[i + 1].Semester, IsReminderSet = dayT2[i + 1].IsReminderSet, FallsIn = "Up-Comming" });
                        }
                        if (i + 2 < dayT2.Count)
                        {
                            Summary.Add(new Subject { Name = dayT2[i + 2].Name, Period = dayT2[i + 2].Period, Venue = dayT2[i + 2].Venue, Time = dayT2[i + 2].Time, Semester = dayT2[i + 2].Semester, IsReminderSet = dayT2[i + 2].IsReminderSet, FallsIn = "Later on" });
                        }
                        if (i + 3 < dayT2.Count)
                        {
                            Summary.Add(new Subject { Name = dayT2[i + 3].Name, Period = dayT2[i + 3].Period, Venue = dayT2[i + 3].Venue, Time = dayT2[i + 3].Time, Semester = dayT2[i + 3].Semester, IsReminderSet = dayT2[i + 3].IsReminderSet, FallsIn = "Later on" });
                        }
                        break;
                    }
                    if (DateTime.Now.ToString("tt") == "PM" && DateTime.Now.Hour >= 6)
                    {
                        subs = db.GetDay(DateTime.Now.AddDays(1).DayOfWeek.ToString(), DateTime.Now.Month > 6 ? 2 : 1);
                        if (subs == null) return;
                        dayT2 = subs.Subjects.ToList();
                        Summary.Add(new Subject { Name = dayT2[i].Name, Period = dayT2[i].Period, Semester = dayT2[i].Semester, Time = dayT2[i].Time, Venue = dayT2[i].Venue, FallsIn = "Tommorrow" });
                        break;
                    }
                    else if ((DateTime.Now.ToString("tt") == "AM" && DateTime.Now.TimeOfDay <= new TimeSpan(7, 45, 01) && DateTime.Now.TimeOfDay >= new TimeSpan(00, 00, 01)))
                    {
                        AnyChange = true;
                        Summary.Add(new Subject { Name = dayT2[i].Name, Period = dayT2[i].Period, Venue = dayT2[i].Venue, Time = dayT2[i].Time, Semester = dayT2[i].Semester, IsReminderSet = dayT2[i].IsReminderSet, FallsIn = "First Lecture/Practical/Tutorial" });
                        if (i + 1 < dayT2.Count)
                        {
                            Summary.Add(new Subject { Name = dayT2[i + 1].Name, Period = dayT2[i + 1].Period, Venue = dayT2[i + 1].Venue, Time = dayT2[i + 1].Time, Semester = dayT2[i + 1].Semester, IsReminderSet = dayT2[i + 1].IsReminderSet, FallsIn = "Followed by" });
                        }
                        if (i + 2 < dayT2.Count)
                        {
                            Summary.Add(new Subject { Name = dayT2[i + 2].Name, Period = dayT2[i + 2].Period, Venue = dayT2[i + 2].Venue, Time = dayT2[i + 2].Time, Semester = dayT2[i + 2].Semester, IsReminderSet = dayT2[i + 2].IsReminderSet, FallsIn = "Later on" });
                        }
                        break;
                    }
                }
            }
        }
        public void UpdateView()
        {

        }
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Ons the property changed.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		void OnPropertyChanged(string propertyName)
		{
			//PropertyChangedEventHandler eventHandler = this.PropertyChanged;
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

			}
		}
	}
}
