using Xamarin.Forms;
using RUTimeTable;
using Plugin.Notifications;
using TK.CustomMap.Api.Google;
using RUTimetable.Classes;

namespace RUTimetable
{
	public class MainPageCS : TabbedPage
	{
		public MainPageCS()
		{
			InflateViews();
		}
		/// <summary>
		/// Inflates the views.
		/// N.B. The MapsMainPage will be added as soon as Xamarin fixes issue where
		/// GooglePlay services v42 works with the latest version of xamarin forms
		/// </summary>
		public void InflateViews()
		{
			Children.Clear();
			GmsDirection.Init("AIzaSyCjMY_194mgeHLsyhlPre7kZ-UVXHCCt0o");
            Children.Add(new NavigationPage(new DaySummary()));
			Children.Add(new NavigationPage(new AddTimetable()));
			Children.Add(new NavigationPage(new Monday()));
			Children.Add(new NavigationPage(new Tuesday( )));
			Children.Add(new NavigationPage(new Wednesday( )));
			Children.Add(new NavigationPage(new Thursday( )));
			Children.Add(new NavigationPage(new Friday()));
			//Children.Add(new NavigationPage(new AddSubjectToVenue()));
            //Children.Add(new NavigationPage(new UpdateSubjectTaken()));
            //Children.Add(new NavigationPage(new AddNewVenue()));
            //Children.Add(new NavigationPage(new MapPageCS()));  N.B. NO longer needed as PopUp view has been used for this 
		}
		void Settings(object sender, System.EventArgs e)
		{

		}
		void Semester1(object sender, System.EventArgs e)
		{

		}
		void Semester2(object sender, System.EventArgs e)
		{

		}
		protected override void OnAppearing()
		{
            InflateViews();
		}

	}
}

