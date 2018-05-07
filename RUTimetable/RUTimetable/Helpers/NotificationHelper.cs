using System;
using System.Collections.Generic;
using Plugin.LocalNotifications;
using Plugin.Notifications;
using Xamarin.Forms;
namespace RUTimetable
{
	public class NotificationHelper 
	{
		public NotificationHelper()
		{
		}
		public void ScheduleNotification(SubjectReminder s,int seconds)
		{
			var Dayprovider = new DayProvider();
			var dayconverter = new DayConverter();
			var message = "Your Lecture is starting shortly!";
			var message1 = "Your Practical is starting shortly!";
			var message2 = "Your tutorial is starting shortly!";
			var message3 = "The tutorial/practical you tutor is starting shortly!";
            var time = s.Subject.Time.Split(':');
            DateTime nextday = Dayprovider.GetNextWeekday(DateTime.Today, dayconverter.ConvertToDayOfWeek(s.DayOfWeek));
            DateTime newnextday = new DateTime(nextday.Year, nextday.Month, nextday.Day, Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));
            newnextday.AddSeconds(-seconds);
            if (DateTime.Today.DayOfWeek.ToString().Split(',')[0] == s.DayOfWeek)
			{
				
				if (s.Subject.Name.Contains("TUTOR"))
				{
					//CrossLocalNotifications.Current.Show(s.Subject.Name, message3, s.ReminderID, newnextday);
					CrossNotifications.Current.Send(new Notification
					{
						Title = s.Subject.Name,
						Message = message3,
						Vibrate = true,
						When = newnextday.TimeOfDay,
						Sound="definite",
                        Date = newnextday

                    });
				}
				if (s.Subject.Name.Contains("PRACTICAL"))
				{
					//CrossLocalNotifications.Current.Show(s.Subject.Name, message1, s.ReminderID, newnextday);
				    CrossNotifications.Current.Send(new Notification
					{
						Title = s.Subject.Name,
						Message = message1,
						Vibrate = true,
						When = newnextday.TimeOfDay,
						Sound="definite",
                        Date = newnextday

                    });
				}
				if (s.Subject.Name.Contains("TUTORIAL"))
				{
					//CrossLocalNotifications.Current.Show(s.Subject.Name, message2, s.ReminderID, newnextday);
				    CrossNotifications.Current.Send(new Notification
					{
						Title = s.Subject.Name,
						Message = message2,
						Vibrate = true,
						When = newnextday.TimeOfDay,
						Sound="definite",
                        Date=newnextday
						                 
					});
				}
				if (s.Subject.Name.Contains("LECTURE"))
				{
					//CrossLocalNotifications.Current.Show(s.Subject.Name, message, s.ReminderID, newnextday);
				    CrossNotifications.Current.Send(new Notification
					{
						Title = s.Subject.Name,
						Message = message,
						Vibrate = true,
						When = newnextday.TimeOfDay,
						Sound="definite",
                        Date = newnextday

                    });
				}
			}
			else
			{
				if (s.Subject.Name.Contains("TUTOR"))
				{
					//CrossLocalNotifications.Current.Show(s.Subject.Name, message3, s.ReminderID, newnextday);
				   	CrossNotifications.Current.Send(new Notification
					{
						Title = s.Subject.Name,
						Message = message3,
						Vibrate = true,
						When = newnextday.TimeOfDay,
						Sound="definite",
                        Date = newnextday

                    });
				}
				if (s.Subject.Name.Contains("PRACTICAL"))
				{
					//CrossLocalNotifications.Current.Show(s.Subject.Name, message1, s.ReminderID, newnextday);
				    CrossNotifications.Current.Send(new Notification
					{
						Title = s.Subject.Name,
						Message = message1,
						Vibrate = true,
						When = newnextday.TimeOfDay,
						Sound="definite",
                        Date = newnextday

                    });
				}
				if (s.Subject.Name.Contains("TUTORIAL"))
				{
					//CrossLocalNotifications.Current.Show(s.Subject.Name, message2, s.ReminderID, newnextday);
				    CrossNotifications.Current.Send(new Notification
					{
						Title = s.Subject.Name,
						Message = message2,
						Vibrate = true,
						When = newnextday.TimeOfDay,
						Sound="definite",
                        Date = newnextday

                    });
				}
				if (s.Subject.Name.Contains("LECTURE"))
				{
					//CrossLocalNotifications.Current.Show(s.Subject.Name, message, s.ReminderID, newnextday);
				    CrossNotifications.Current.Send(new Notification
					{
						Title = s.Subject.Name,
						Message = message,
						Vibrate = true,
						When = newnextday.TimeOfDay,
						Sound="definite",
                        Date = newnextday

                    });
				}
			}
		}
		public void CancelNotification(SubjectReminder s)
		{
			CrossNotifications.Current.Cancel(s.ReminderID);
		}

	}
}
