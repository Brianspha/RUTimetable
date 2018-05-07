using System;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace RUTimetable
{
	public class GeoLocationProvider
	{
		double LAT, LONG;
        Position pos;
        string address;
		public GeoLocationProvider()
		{
			GetLocation();
		}
        public GeoLocationProvider(Position pos)
        {
            this.pos = pos;
            GetLocation();
        }
        public async void GetLocation()
		{
			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;
			var position = await locator.GetPositionAsync(new TimeSpan(50000));
			LAT = position.Latitude;
			LONG = position.Longitude;
		}
        public async void GetStreetAddress()
        {
            Geocoder geoCoder=new Geocoder();
            var position = new Position(pos.Latitude, pos.Longitude);
            var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(pos);
            foreach (var add in possibleAddresses)
            {
                address = add;
                break;
            }
        }
		public LatLongHolder GetLatLong()
		{
			return new LatLongHolder { Latitude = LAT, Longitude = LONG };
		}
        public string GetStreetAdd()
        {
            return address;
        }
	}
}
