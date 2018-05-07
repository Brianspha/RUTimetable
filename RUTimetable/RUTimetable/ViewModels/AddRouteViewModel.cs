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
	public class AddRouteViewModel
	{
		private IPlaceResult fromPlace, toPlace;
		private TK.CustomMap.Position from, to;
        RealmDataBase db;
		public ObservableCollection<TKCustomMapPin> Pins { get; private set; }
		public ObservableCollection<TKRoute> Routes { get; private set; }
		public TK.CustomMap.MapSpan Bounds { get; private set; }

		public Command<IPlaceResult> FromSelectedCommand
		{
			get
			{
                var location = db.GetDirections();
				return new Command<IPlaceResult>(async (p) =>
				{
					if (Device.RuntimePlatform == Device.iOS)
					{
						TKNativeiOSPlaceResult placeResult = (TKNativeiOSPlaceResult)p;
						this.fromPlace = placeResult;
						this.from = placeResult.Details.Coordinate;
					}
					else
					{
						TKNativeAndroidPlaceResult placeResult = (TKNativeAndroidPlaceResult)p;
						this.fromPlace = placeResult;
						var details = await TKNativePlacesApi.Instance.GetDetails(placeResult.PlaceId);

						this.from = details.Coordinate;
					}
				});
			}
		}
		public Command<IPlaceResult> ToSelectedCommand
		{
			get
			{
				return new Command<IPlaceResult>(async (p) =>
				{
					if (Device.RuntimePlatform == Device.iOS)
					{
						TKNativeiOSPlaceResult placeResult = (TKNativeiOSPlaceResult)p;
						this.toPlace = placeResult;
						this.to = placeResult.Details.Coordinate;
					}
					else
					{
						TKNativeAndroidPlaceResult placeResult = (TKNativeAndroidPlaceResult)p;
						this.toPlace = placeResult;
						var details = await TKNativePlacesApi.Instance.GetDetails(placeResult.PlaceId);

						this.to = details.Coordinate;
					}
				});
			}
		}

		public Command AddRouteCommand
		{
			get
			{
				return new Command(() =>
				{
					if (this.toPlace == null || this.fromPlace == null) return;

					var route = new TKRoute
					{
						TravelMode = TKRouteTravelMode.Driving,
						Source = this.from,
						Destination = this.to,
						Color = Color.Blue
					};

					this.Pins.Add(new RoutePin
					{
						Route = route,
						IsSource = true,
						IsDraggable = true,
						Position = this.from,
						Title = this.fromPlace.Description,
						ShowCallout = true,
						DefaultPinColor = Color.Green
					});
					this.Pins.Add(new RoutePin
					{
						Route = route,
						IsSource = false,
						IsDraggable = true,
						Position = this.to,
						Title = this.toPlace.Description,
						ShowCallout = true,
						DefaultPinColor = Color.Red
					});

					this.Routes.Add(route);

					});
			}
		}

		public AddRouteViewModel(ObservableCollection<TKRoute> routes, ObservableCollection<TKCustomMapPin> pins, TK.CustomMap.MapSpan bounds)
		{
			this.Routes = routes;
			this.Pins = pins;
			this.Bounds = bounds;
            db = new RealmDataBase();
        }
	}
}