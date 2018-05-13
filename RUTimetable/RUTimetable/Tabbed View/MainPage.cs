using Xamarin.Forms;
using RUTimeTable;
using Plugin.Notifications;
using TK.CustomMap.Api.Google;
using RUTimetable.Classes;
using Syncfusion.XForms.TabView;
using Xamarin.Forms.Xaml;

namespace RUTimetable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class MainPageCS : ContentPage
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
            GmsDirection.Init("AIzaSyCjMY_194mgeHLsyhlPre7kZ-UVXHCCt0o");
            SfTabItem mnday = new SfTabItem();
            mnday.Content = new Monday();
            mnday.Title = "Monday";
            mnday.TitleFontColor = Color.Black;
            mnday.Content.BackgroundColor = Color.WhiteSmoke;
            SfTabItem tues = new SfTabItem();
            tues.Title = "Tuesday";
            tues.TitleFontColor = Color.Black;
            tues.Content = new Tuesday();
            tues.Content.BackgroundColor = Color.WhiteSmoke;
            SfTabItem wed = new SfTabItem();
            wed.Title = "Wednesday";
            wed.TitleFontColor = Color.Black;
            wed.Content = new Wednesday();
            wed.Content.BackgroundColor = Color.WhiteSmoke;
            SfTabItem thurs = new SfTabItem();
            thurs.Title = "Thursday";
            thurs.TitleFontColor = Color.Black;
            thurs.Content = new Thursday();
            thurs.Content.BackgroundColor = Color.WhiteSmoke;
            SfTabItem fri = new SfTabItem();
            fri.Title = "Friday";
            fri.TitleFontColor = Color.Black;
            fri.Content = new Friday();
            fri.Content.BackgroundColor = Color.WhiteSmoke;
            SfTabItem daySummary = new SfTabItem();
            daySummary.Title = "Day Summary";
            daySummary.TitleFontColor = Color.Black;
            daySummary.Content = new DaySummary();
            SfTabItem addTimetable = new SfTabItem();
            addTimetable.Title = "Add Timetable";
            addTimetable.TitleFontColor = Color.Black;
            addTimetable.Content = new AddTimetable();
            SfTabItem Main = new SfTabItem();
            Main.Title = "Menu";
            Main.TitleFontColor = Color.Black;
            Main.Content = new SfTabView {
                DisplayMode=TabDisplayMode.ImageWithText,
                VisibleHeaderCount = 2,
                BackgroundColor = Color.White,
                EnableSwiping=true,
                Items = new TabItemCollection { daySummary, addTimetable },
            };
            Grid main = new Grid { BackgroundColor =Color.White};
            main.Children.Add(new SfTabView() { DisplayMode = TabDisplayMode.ImageWithText, EnableSwiping = true,BackgroundColor = Color.WhiteSmoke, Items = new TabItemCollection { Main, mnday, tues, wed, thurs, fri } });
            Content = main;
           
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

	}
}

