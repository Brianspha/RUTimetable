
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
using Plugin.LocalNotifications;

namespace RUTimetable.Droid
{
	[BroadcastReceiver(Enabled = true)]
	[IntentFilter(new[] { Intent.ActionBootCompleted})]
	public class ReUploadNotificationsBroadCastReciever : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			//CrossLocalNotifications.Current.Show("Boot","Service Started",0);
			Toast.MakeText(context, "RUTimetable started!", ToastLength.Long).Show();
			//context.StartService(new Intent(context,typeof(ReUploadNotifications)));
		}
	}
}
