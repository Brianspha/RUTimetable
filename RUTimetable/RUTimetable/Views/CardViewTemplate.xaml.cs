using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading;
using GeoJSON.Net;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Extensions;
using RUTimetable.Views;

namespace RUTimetable
{
	public partial class CardViewTemplate : ContentView
	{
		string day = "";//used for setting dayof week reminder falls in
		Subject sub;
		public CardViewTemplate()
		{
			InitializeComponent();
		}
		/// <summary>
		/// Views Subject Venue location on Campus Map.
		/// This feature is still under development not really it works 
		/// just xamarin forms hasnt fixed the issue with google maps not
		/// working on their latest version
		/// Im till trying to move this into a ViewModel
		/// </summary>
		public async void ViewOnCampusMap(object sender, EventArgs e)
		{
			var subject = Name.Text;
			GeoLocationProvider loc = new GeoLocationProvider();
			loc.GetLocation();
			var currentLocation = loc.GetLatLong();
			var db = new RealmDataBase();
			var venue = db.GetVenueLocationBySubject(subject);
			db.SaveDirections(new DirectionsRequest { A = new CustomPosition { Name = "You", Lat = currentLocation.Latitude, Long = currentLocation.Longitude }, B = new CustomPosition { Name=venue.Name,Lat=venue.Lat,Long=venue.Long} });
            await Navigation.PushPopupAsync(new ViewOnCampusMapPopUp());
        }
		/// <summary>
		///Updates the Switch toggle state.
		/// Unfortunately im not yet able to bind this as a command just 
		/// But will soon as i figure it out
		/// </summary>
		/// <param name="sender">Sender.</param>
		void SwitchToggled(object sender, ToggledEventArgs e)
		{
			sub = new Subject { Name = Name.Text, Time = Time.Text, Period = Period.Text,Semester=DateTime.Now.Month>6?2:1 };
			var helper = new ReminderHelper();
    		day = new RealmDataBase().GetDay(sub);
			if (Switch.IsToggled)
			{
				helper.SetReminder(sub,day);
			}
			else
			{
				helper.RemoveReminder(sub, day);
			}
		}
		void View_Clicked(object sender, System.EventArgs e)
		{

		}
	}
}
