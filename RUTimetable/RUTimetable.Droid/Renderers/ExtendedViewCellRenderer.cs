using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using RUTimetable;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0436 // Type conflicts with imported type
[assembly: ExportRenderer(typeof(ExtendedViewCell), target: typeof(ExtendedViewCellRenderer))]
#pragma warning restore CS0436 // Type conflicts with imported type
namespace RUTimetable
{
	public class ExtendedViewCellRenderer : ViewCellRenderer
	{
		protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
		{
			var cell = base.GetCellCore(item, convertView, parent, context);
			var listView = parent as Android.Widget.ListView;

			if (listView != null)
			{
				// Disable native cell selection color style - set as *Transparent*
				listView.SetSelector(Android.Resource.Color.Transparent);
				listView.CacheColorHint = Android.Graphics.Color.Transparent;
			}

			return cell;
		}
	}
}
