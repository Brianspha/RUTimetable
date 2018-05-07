using Xamarin.Forms;
using RUTimeTable;
using Plugin.Notifications;
using System.Collections.Generic;
using TK.CustomMap.Api.Google;
using RUTimetable.Views;
using System;

namespace RUTimetable
{
	public partial class App : Application
	{
		public static double ScreenHeight;
		public static double ScreenWidth;
        SemesterChangeHandler ChangeHandler;
        //Used by Notifications Plugin
        public static bool IsInBackgrounded { get; private set; }

        public App(GeoJSONData dat,List<VenueLocation> venues,bool fromIOS)
		{
			InitializeComponent();
            ChangeHandler = new SemesterChangeHandler();
			GmsPlace.Init("AIzaSyCjMY_194mgeHLsyhlPre7kZ-UVXHCCt0o");
			GmsDirection.Init("AIzaSyCJN3Cd-Sp1a5V5OnkvTR-Gqhx7A3S-b6M");
			var temp =new GEOJSONTOJSONParser(dat,venues);//the Dat file sent by each platform to the parent App
			temp.Process();
            if (fromIOS)
            {
                var carouselView = new CarouselPage();
                carouselView.Children.Add(new DaySummary());
                carouselView.Children.Add(new AddTimetable());
                carouselView.Children.Add(new Monday());
                carouselView.Children.Add(new Tuesday());
                carouselView.Children.Add(new Wednesday());
                carouselView.Children.Add(new Thursday());
                carouselView.Children.Add(new Friday());
                carouselView.ToolbarItems.Add(new ToolbarItem("Semester 1", null, new Action(() => Semester1()), ToolbarItemOrder.Secondary));
                carouselView.ToolbarItems.Add(new ToolbarItem("Semester 2", null, new Action(() => Semester2()), ToolbarItemOrder.Secondary));
                carouselView.ToolbarItems.Add(new ToolbarItem("Settings", null, new Action(() => SettingsAsync()), ToolbarItemOrder.Secondary));
                MainPage = carouselView;
            }
			else MainPage = new MainPageCS();
		}
		public App(bool fromIOS)
		{
            InitializeComponent();
            ChangeHandler = new SemesterChangeHandler();
            GmsPlace.Init("AIzaSyCjMY_194mgeHLsyhlPre7kZ-UVXHCCt0o");
            GmsDirection.Init("AIzaSyCJN3Cd-Sp1a5V5OnkvTR-Gqhx7A3S-b6M");
            if (fromIOS)
            {
                var carouselView = new CarouselPage();
                carouselView.Children.Add(new DaySummary());
                carouselView.Children.Add(new AddTimetable());
                carouselView.Children.Add(new Monday());
                carouselView.Children.Add(new Tuesday());
                carouselView.Children.Add(new Wednesday());
                carouselView.Children.Add(new Thursday());
                carouselView.Children.Add(new Friday());
                carouselView.ToolbarItems.Add(new ToolbarItem("Semester 1", null, new Action(() => Semester1()), ToolbarItemOrder.Secondary));
                carouselView.ToolbarItems.Add(new ToolbarItem("Semester 2", null, new Action(() => Semester2()), ToolbarItemOrder.Secondary));
                carouselView.ToolbarItems.Add(new ToolbarItem("Settings", null, new Action(() => SettingsAsync()), ToolbarItemOrder.Secondary));
                MainPage = carouselView;
            }
            else MainPage = new MainPageCS();
		}


		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
            App.IsInBackgrounded = true;

            // Handle when your app sleeps
        }

        protected override void OnResume()
		{
            App.IsInBackgrounded = false;

            // Handle when your app resumes
        }
        /// <summary>
        /// Semester1 the specified sender and e.
        /// </summary>
        /// <returns>The semester1.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void Semester1()
        {
            ChangeHandler.CommunicateWithOtherTabs(1);
        }
        /// <summary>
        /// Semester2 the specified sender and e.
        /// </summary>
        /// <returns>The semester2.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void Semester2()
        {
            ChangeHandler.CommunicateWithOtherTabs(2);
        }
        /// <summary>
        /// Settings the specified sender and e.
        /// </summary>
        /// <returns>The settings.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
		public async void SettingsAsync()
        {
            //await PushPopupAsync(new Settings());
        }

    }
}
