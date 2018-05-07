using System;
namespace RUTimetable
{
	public class ReminderHelper
	{
		RealmDataBase db;
		NotificationHelper helper;
		public ReminderHelper()
		{
			db = new RealmDataBase();
			helper= new NotificationHelper();
		}
		public void SetReminder(Subject sub, string dayOfWeek)
		{

			var modified = new Subject { Name=sub.Name,Time=new TimeProvider().ConvertLectureTimeToAlarmTime(sub.Time),Period=sub.Period,Semester=sub.Semester};
			var reminder = new SubjectReminder { Subject = modified, DayOfWeek = dayOfWeek, Semester = sub.Semester, OnOf = true };
			db.SetReminder(reminder);
			helper.ScheduleNotification(reminder,db.GetMinutesToSendBeforeTime());
		}
		public void RemoveReminder(Subject sub, string dayOfWeek)
		{
			var modified = new Subject { Name = sub.Name, Time = new TimeProvider().ConvertLectureTimeToAlarmTime(sub.Time), Period = sub.Period, Semester = sub.Semester};
			var reminder = new SubjectReminder { Subject = modified, DayOfWeek = dayOfWeek, Semester = sub.Semester, OnOf = true };
			db.RemoveReminder(reminder);
			helper.CancelNotification(reminder);
		    
		}
		public bool GetReminderState(Subject sub, string dayOfWeek)
		{
			var modified = new Subject { Name = sub.Name, Time = new TimeProvider().ConvertLectureTimeToAlarmTime(sub.Time), Period = sub.Period, Semester = sub.Semester};
			var reminder = new SubjectReminder { Subject = modified, DayOfWeek = dayOfWeek, Semester = sub.Semester };
			return db.GetReminderState(reminder);
		}
	}
}
