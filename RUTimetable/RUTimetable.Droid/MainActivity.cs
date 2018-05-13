using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.LocalNotifications;
using Acr.UserDialogs;
using System.Threading.Tasks;
using Plugin.Permissions;
using TK.CustomMap.Droid;
using Rg.Plugins;
using Android.Support.Design.Widget;
namespace RUTimetable.Droid
{
	[Activity(Label = "RUTimetable.Droid", Icon = "@drawable/book", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		const int RequestLocationId = 0;

		readonly string[] PermissionsLocation =
	   {
			  "android.permission.ACCESS_COARSE_LOCATION",
			   "android.permission.ACCESS_FINE_LOCATION"
			};
		Android.Views.View layout;

		protected override async void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;
			base.OnCreate(bundle);
			var renderer = new TKCustomMapRenderer(this);
			global::Xamarin.Forms.Forms.Init(this, bundle);
			LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.book;
			UserDialogs.Init(this);
            TKGoogleMaps.Init(this, bundle);
			var db = new RealmDataBase();
			if (db.FirstRun())
			{
				var temp = new ResourceHelper(this.ApplicationContext, "RhodesMap.geojson", "Venues.txt");
				LoadApplication(new App(temp.ReadLocalFile(), temp.GetParsedVenuesWithSubjects()));
			}
			else
			{
				LoadApplication(new App());
			}
			await GetLocationPermissionAsync();
		}
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}		
		async Task GetLocationPermissionAsync()
		{
			const string permission = "android.permission.ACCESS_FINE_LOCATION"
;

			if (ShouldShowRequestPermissionRationale(permission))
			{
				//Explain to the user why we need to read the contacts
				Snackbar.Make(layout, "Location access is required to show directions to lecture venues.",
					Snackbar.LengthIndefinite)
					.SetAction("OK", v => RequestPermissions(PermissionsLocation, RequestLocationId))
					.Show();

				return;
			}
			for (int i = 0; i < PermissionsLocation.Length; i++) RequestPermissions(PermissionsLocation, i);

		}
	}
}
