using System;
using System.Windows.Input;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace RUTimetable
{
	public class CardViewViewModel
	{
		public ICommand ViewOnCampus { get; private set; }
		public ICommand Toggled { get; private set; }
		string day = "";//used for setting dayof week reminder falls in
		Subject sub;
		public CardViewViewModel()
		{
     		ViewOnCampus = new Command(ViewOnCampusMap);
			Toggled = new Command<ReminderObject>(SwitchToggeled);

		}
		/// <summary>
		/// Views Subject Venue location on Campus Map.
		/// This feature is still under development not really it works 
		/// just xamarin forms hasnt fixed the issue with google maps not
		/// working on their latest version
		/// </summary>
		public async void ViewOnCampusMap()
		{
			
			await UserDialogs.Instance.AlertAsync("Feature Disable Still under Development", "Feature disabled", "OK");
		}
		/// <summary>
		/// Updates Switch toggeled State.
		/// </summary>
		/// //Still to be tested trying to multibind in xamarin is a bit of a challenge
		/// <param name="obj">Object.</param>
		public void SwitchToggeled(ReminderObject obj)
		{
			sub = new Subject { Name = obj.Name.Text, Time = obj.Time.Text, Period = obj.Period.Text,Semester=DateTime.Now.Month>6?2:1 };
			var helper = new ReminderHelper();
			day = new RealmDataBase().GetDay(sub);
			if (obj.zwitch.IsToggled)
			{
				helper.SetReminder(sub,day);
			}
			else
			{
				helper.RemoveReminder(sub, day);
			}
		}
	}
}
