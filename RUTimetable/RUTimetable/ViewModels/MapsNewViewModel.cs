using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms.Maps;
using Refractored.FabControl;

namespace RUTimetable
{
	public class MapsNewViewModel : INotifyPropertyChanged
	{
		private CustomMap customMap;
		RealmDataBase db;
		bool visibleYet { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;
		public bool MapRegionVisible
		{
			get { return visibleYet; }
			set
			{
				if (null != customMap.VisibleRegion)
				{
					visibleYet = true;
					this.OnPropertyChanged("MapRegionVisible");
					SetUpMap();
				}
			}
		}
		public MapsNewViewModel()
		{
			db = new RealmDataBase();
			SetUpMap();
		}
		public void IsVisible() {
			if (customMap.VisibleRegion != null) { SetUpMap();}
		}
		void SetUpMap()
		{
			customMap = new CustomMap
			{
				MapType = MapType.Street,
				WidthRequest = App.ScreenWidth,
				HeightRequest = App.ScreenHeight
			};

			var location = db.GetDirections();
			if (location != null &&location.B.Lat !=0 && location.B.Long != 0) {
				customMap.MoveToRegion(new MapSpan(new Position(location.B.Lat, location.B.Long), 0.001, 0.001));
				customMap.Pins.Add(new Pin { Type = PinType.Place, Label = location.B.Name, Position = new Position(location.B.Lat, location.B.Long) });
			   
			}
			else
			{
				//var loc = new GeoLocationProvider().GetLatLong(); 
				customMap.RouteCoordinates.Add(new Position(-33.311836, 26.520642));
				customMap.MoveToRegion(new MapSpan(new Position(-33.311836, 26.520642), 0.002, 0.001));
			}
			db.RemoveDirectionRequest();

		}
        public bool IsNullOrValue(double valueToCheck)
        {
            return Double.IsNaN(valueToCheck);
        }
        private int getMapZoom()
		{
			var LatLng = (customMap.VisibleRegion.LatitudeDegrees + customMap.VisibleRegion.LongitudeDegrees) / 2.0f;
			int zoom = (int)Math.Floor(Math.Log(360 / LatLng, 2));
			return zoom;
		}
		private double setMapZoom(int nZoom)
		{
			if (nZoom < 1 || nZoom > 18)
			{
				return 0;
			}

			var latlongdeg = 360 / (Math.Pow(2, nZoom));
			return latlongdeg;
		}
		/// <summary>
		/// Ons the property changed.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		void OnPropertyChanged(string propertyName)
		{
			//PropertyChangedEventHandler eventHandler = this.PropertyChanged;
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		public void MoveToLocation(VenueLocation loc){
				customMap.MoveToRegion(new MapSpan(new Position(loc.Lat, loc.Long), 0.002, 0.001));
			customMap.Pins.Add(new Pin { Type = PinType.Place, Label = loc.Name, Position = new Position(loc.Lat, loc.Long) });			   
		}
        public CustomMap GetCustomMap()
        {
            return customMap;
        }
	}
}
