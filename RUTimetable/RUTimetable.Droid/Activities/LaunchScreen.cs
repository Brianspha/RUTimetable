using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace RUTimetable.Droid.Activities
{
    [Activity(Label = "RUTimetable", NoHistory = true, Icon = "@drawable/book", Theme = "@style/Theme.Splash", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class LaunchScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            System.Threading.Thread.Sleep(3000);
            StartActivity(typeof(MainActivity));
            this.OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.SlideOutRight);
            // Create your application here
        }
    }
}