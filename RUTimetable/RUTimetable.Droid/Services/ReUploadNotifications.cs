
using System;

using Android.App;
using Android.Content;
using Android.OS;
using RUTimetable;
using Plugin.LocalNotifications;
namespace RUTimetable.Droid
{
	[Service(Label = "ReUploadNotifications")]
	[IntentFilter(new String[] { "com.yourname.ReUploadNotifications" })]
	public class ReUploadNotifications : Service
	{
		IBinder binder;

		public override StartCommandResult OnStartCommand(Android.Content.Intent intent, StartCommandFlags flags, int startId)
		{
			// start your service logic here
			//RealmDataBase db = new RealmDataBase();
			//var reminders = db.GetReminders();
			//NotificationHelper helper = new NotificationHelper();
			//if (reminders != null)
			//{
			//	for (int i = 0; i < reminders.Count; i++)
			//	{
			//		var not = reminders[i];
			//		helper.ScheduleNotification(not);
			//	}
			//}
			// Return the correct StartCommandResult for the type of service you are building
			return StartCommandResult.StickyCompatibility;
		}

		public override IBinder OnBind(Intent intent)
		{
			binder = new ReUploadNotificationsBinder(this);
			return binder;
		}
	}

	public class ReUploadNotificationsBinder : Binder
	{
		readonly ReUploadNotifications service;

		public ReUploadNotificationsBinder(ReUploadNotifications service)
		{
			this.service = service;
		}

		public ReUploadNotifications GetReUploadNotifications()
		{
			return service;
		}
	}
}
