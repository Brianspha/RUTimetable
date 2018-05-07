using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap.Api;
using TK.CustomMap.Overlays;
using Xamarin.Forms;
using TK.CustomMap;
using Xamarin.Forms.Maps;

namespace RUTimetable
{
	public partial class LectureVenueLocatorPage : ContentPage
	{
		public LectureVenueLocatorPage(ObservableCollection<TKRoute> routes, ObservableCollection<TKCustomMapPin> pins, TK.CustomMap.MapSpan bounds)
		{
			InitializeComponent();
			BindingContext = new AddRouteViewModel(routes,pins,bounds);
		}
	}
}
