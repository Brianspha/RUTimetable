using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TK.CustomMap.Api;
using TK.CustomMap.Api.Google;
using TK.CustomMap.Api.OSM;
using TK.CustomMap.Interfaces;
using TK.CustomMap.Overlays;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using TK.CustomMap;
namespace RUTimetable
{
	public class MapsViewModel : INotifyPropertyChanged
	{
		private TKTileUrlOptions _tileUrlOptions;

		private TK.CustomMap.MapSpan _mapRegion;
		private TK.CustomMap.Position _mapCenter;
		private TKCustomMapPin _selectedPin;
		private ObservableCollection<TKCustomMapPin> _pins;
		private ObservableCollection<TKRoute> _routes;
		private ObservableCollection<TKCircle> _circles;
		private ObservableCollection<TKPolyline> _lines;
		private ObservableCollection<TKPolygon> _polygons;

		public TKTileUrlOptions TilesUrlOptions
		{
			get
			{
				return this._tileUrlOptions;
				//return new TKTileUrlOptions(
				//    "http://a.basemaps.cartocdn.com/dark_all/{2}/{0}/{1}.png", 256, 256, 0, 18);
				//return new TKTileUrlOptions(
				//    "http://a.tile.openstreetmap.org/{2}/{0}/{1}.png", 256, 256, 0, 18);
			}
			set
			{
				if (this._tileUrlOptions != value)
				{
					this._tileUrlOptions = value;
					this.OnPropertyChanged("TilesUrlOptions");
				}
			}
		}

		public IRendererFunctions MapFunctions { get; set; }


		public Command ShowListCommand
		{
			get
			{
				return new Command(async () =>
				{
					if (this._pins == null || !this._pins.Any())
					{
						await Application.Current.MainPage.DisplayAlert("Nothing there!", "No pins to show!", "OK");
						return;
					}
					var listPage = new PinsListPage(this.Pins);
					listPage.PinSelected += async (o, e) =>
					{
						this.SelectedPin = e.Pin;
						await Application.Current.MainPage.Navigation.PopAsync();
					};
					await Application.Current.MainPage.Navigation.PushAsync(listPage);
				});
			}
		}

		/// <summary>
		/// Map region bound to <see cref="TKCustomMap"/>
		/// </summary>
		public TK.CustomMap.MapSpan MapRegion
		{
			get { return this._mapRegion; }
			set
			{
				if (this._mapRegion != value)
				{
					this._mapRegion = value;
					this.OnPropertyChanged("MapRegion");
				}
			}
		}

		/// <summary>
		/// Pins bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public ObservableCollection<TKCustomMapPin> Pins
		{
			get { return this._pins; }
			set
			{
				if (this._pins != value)
				{
					this._pins = value;
					this.OnPropertyChanged("Pins");
				}
			}
		}

		/// <summary>
		/// Routes bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public ObservableCollection<TKRoute> Routes
		{
			get { return this._routes; }
			set
			{
				if (this._routes != value)
				{
					this._routes = value;
					this.OnPropertyChanged("Routes");
				}
			}
		}

		/// <summary>
		/// Circles bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public ObservableCollection<TKCircle> Circles
		{
			get { return this._circles; }
			set
			{
				if (this._circles != value)
				{
					this._circles = value;
					this.OnPropertyChanged("Circles");
				}
			}
		}

		/// <summary>
		/// Lines bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public ObservableCollection<TKPolyline> Lines
		{
			get { return this._lines; }
			set
			{
				if (this._lines != value)
				{
					this._lines = value;
					this.OnPropertyChanged("Lines");
				}
			}
		}

		/// <summary>
		/// Polygons bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public ObservableCollection<TKPolygon> Polygons
		{
			get { return this._polygons; }
			set
			{
				if (this._polygons != value)
				{
					this._polygons = value;
					this.OnPropertyChanged("Polygons");
				}
			}
		}

		/// <summary>
		/// Map center bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public TK.CustomMap.Position MapCenter
		{
			get { return this._mapCenter; }
			set
			{
				if (this._mapCenter != value)
				{
					this._mapCenter = value;
					this.OnPropertyChanged("MapCenter");
				}
			}
		}

		/// <summary>
		/// Selected pin bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public TKCustomMapPin SelectedPin
		{
			get { return this._selectedPin; }
			set
			{
				if (this._selectedPin != value)
				{
					this._selectedPin = value;
					this.OnPropertyChanged("SelectedPin");
				}
			}
		}

		/// <summary>
		/// Map Long Press bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public Command<TK.CustomMap.Position> MapLongPressCommand
		{
			get
			{
				return new Command<TK.CustomMap.Position>(async position =>
				{
					var action = await Application.Current.MainPage.DisplayActionSheet(
						"Long Press",
						"Cancel",
						null,
						"Add Pin",
						"Add Circle");

					if (action == "Add Pin")
					{
						var pin = new TKCustomMapPin
						{
							Position = position,
							Title = string.Format("Pin {0}, {1}", position.Latitude, position.Longitude),
							ShowCallout = true,
							IsDraggable = true
						};
						this._pins.Add(pin);
					}
					else if (action == "Add Circle")
					{
						var circle = new TKCircle
						{
							Center = position,
							Radius = 10000,
							Color = Color.FromRgba(100, 0, 0, 80)
						};
						this._circles.Add(circle);
					}
				});
			}
		}

		/// <summary>
		/// Map Clicked bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public Command<TK.CustomMap.Position> MapClickedCommand
		{
			get
			{
				return new Command<TK.CustomMap.Position>((positon) =>
				{
					this.SelectedPin = null;

					// Determine if a point was inside a circle
					if ((from c in this._circles let distanceInMeters = c.Center.DistanceTo(positon) * 1000 where distanceInMeters <= c.Radius select c).Any())
					{
						Application.Current.MainPage.DisplayAlert("Circle tap", "Circle was tapped", "OK");
					}
				});
			}
		}

		/// <summary>
		/// Command when a place got selected
		/// </summary>
		public Command<IPlaceResult> PlaceSelectedCommand
		{
			get
			{
				return new Command<IPlaceResult>(async p =>
				{
					var gmsResult = p as GmsPlacePrediction;
					if (gmsResult != null)
					{
						var details = await GmsPlace.Instance.GetDetails(gmsResult.PlaceId);
						this.MapCenter = new TK.CustomMap.Position(details.Item.Geometry.Location.Latitude, details.Item.Geometry.Location.Longitude);
						return;
					}
					var osmResult = p as OsmNominatimResult;
					if (osmResult != null)
					{
						this.MapCenter = new TK.CustomMap.Position(osmResult.Latitude, osmResult.Longitude);
						return;
					}

					if (Device.OS == TargetPlatform.Android)
					{
						var prediction = (TKNativeAndroidPlaceResult)p;

						var details = await TKNativePlacesApi.Instance.GetDetails(prediction.PlaceId);

						this.MapCenter = details.Coordinate;
					}
					else if (Device.OS == TargetPlatform.iOS)
					{
						var prediction = (TKNativeiOSPlaceResult)p;

						this.MapCenter = prediction.Details.Coordinate;
					}
				});
			}
		}

		/// <summary>
		/// Pin Selected bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public Command PinSelectedCommand
		{
			get
			{
				return new Command<TKCustomMapPin>((TKCustomMapPin pin) =>
				{
					// Chose one

					// 1. First possibility
					//this.MapCenter = this.SelectedPin.Position;
					// 2. Possibility
					this.MapRegion = TK.CustomMap.MapSpan.FromCenterAndRadius(this.SelectedPin.Position, this.MapRegion.Radius);
					// 3. Possibility
					//this.MapFunctions.MoveToMapRegion(
					//    MapSpan.FromCenterAndRadius(this.SelectedPin.Position, Distance.FromMeters(this.MapRegion.Radius.Meters)),
					//    true);
				});
			}
		}

		/// <summary>
		/// Drag End bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public Command<TKCustomMapPin> DragEndCommand
		{
			get
			{
				return new Command<TKCustomMapPin>(pin =>
				{
					var routePin = pin as RoutePin;

					if (routePin != null)
					{
						if (routePin.IsSource)
						{
							routePin.Route.Source = pin.Position;
						}
						else
						{
							routePin.Route.Destination = pin.Position;
						}
					}
				});
			}
		}


		/// <summary>
		/// Callout clicked bound to the <see cref="TKCustomMap"/>
		/// </summary>
		public Command CalloutClickedCommand
		{
			get
			{
				return new Command<TKCustomMapPin>(async (TKCustomMapPin pin) =>
				{
					var action = await Application.Current.MainPage.DisplayActionSheet(
						"Callout clicked",
						"Cancel",
						"Remove Pin");

					if (action == "Remove Pin")
					{
						this._pins.Remove(pin);
					}
				});
			}
		}

		public Command ClearMapCommand
		{
			get
			{
				return new Command(() =>
				{
					this._pins.Clear();
					this._circles.Clear();
					if (this._routes != null)
						this._routes.Clear();
				});
			}
		}

		/// <summary>
		/// Navigate to a new page to get route source/destination
		/// </summary>
		public Command AddRouteCommand
		{
			get
			{
				return new Command(() =>
				{
					if (this.Routes == null) this.Routes = new ObservableCollection<TKRoute>();

					var addRoutePage = new LectureVenueLocatorPage(this.Routes, this.Pins, this.MapRegion);
					Application.Current.MainPage.Navigation.PushAsync(addRoutePage);
				});
			}
		}

		/// <summary>
		/// Command when a route calculation finished
		/// </summary>
		public Command<TKRoute> RouteCalculationFinishedCommand
		{
			get
			{
				return new Command<TKRoute>(r =>
				{
					// move to the bounds of the route
					this.MapRegion = r.Bounds;
				});
			}
		}

		public MapsViewModel()
		{
			this._mapCenter = new TK.CustomMap.Position(-33.311836, 26.520642);

			this._pins = new ObservableCollection<TKCustomMapPin>();
			this._circles = new ObservableCollection<TKCircle>();
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}