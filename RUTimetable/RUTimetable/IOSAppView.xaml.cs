using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap.Api.Google;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RUTimetable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IOSAppView : Application
    {
        public static double ScreenHeight;
        public static double ScreenWidth;
        //Used by Notifications Plugin
        public static bool IsInBackgrounded { get; private set; }

        public IOSAppView(GeoJSONData dat, List<VenueLocation> venues)
        {
            InitializeComponent();
                var carouselView = new CarouselPage();
                carouselView.Children.Add(new DaySummary());
                carouselView.Children.Add(new AddTimetable());
                carouselView.Children.Add(new Monday());
                carouselView.Children.Add(new Tuesday());
                carouselView.Children.Add(new Wednesday());
                carouselView.Children.Add(new Thursday());
                carouselView.Children.Add(new Friday());
                GmsPlace.Init("AIzaSyCjMY_194mgeHLsyhlPre7kZ-UVXHCCt0o");
                GmsDirection.Init("AIzaSyCJN3Cd-Sp1a5V5OnkvTR-Gqhx7A3S-b6M");
                var temp = new GEOJSONTOJSONParser(dat, venues);//the Dat file sent by each platform to the parent App
                temp.Process();
               MainPage = carouselView;
        }

        public IOSAppView()
        {

            GmsPlace.Init("AIzaSyCjMY_194mgeHLsyhlPre7kZ-UVXHCCt0o");
            GmsDirection.Init("AIzaSyCJN3Cd-Sp1a5V5OnkvTR-Gqhx7A3S-b6M");

        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            IOSAppView.IsInBackgrounded = true;

            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            IOSAppView.IsInBackgrounded = false;

            // Handle when your app resumes
        }
    }
}