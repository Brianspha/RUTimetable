using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TK.CustomMap;
using TK.CustomMap.Api;
using TK.CustomMap.Api.Google;
using TK.CustomMap.Api.OSM;
using TK.CustomMap.Interfaces;
using TK.CustomMap.Overlays;
using Xamarin.Forms;
namespace RUTimetable
{
    public class TKCustomMapBindingsViewModel : INotifyPropertyChanged
        {
            TKTileUrlOptions tileUrlOptions;

            MapSpan mapRegion = MapSpan.FromCenterAndRadius(new Position(-33.311836, 26.520642), Distance.FromKilometers(2));
            Position mapCenter;
            TKCustomMapPin selectedPin;
            bool isClusteringEnabled;
            ObservableCollection<TKCustomMapPin> pins;
            ObservableCollection<TKRoute> routes;
            ObservableCollection<TKCircle> circles;
            ObservableCollection<TKPolyline> lines;
            ObservableCollection<TKPolygon> polygons;
            RealmDataBase db;
            private IPlaceResult fromPlace, toPlace;
            private TK.CustomMap.Position from, to;
            public IRendererFunctions MapFunctions { get; set; }

            public bool IsClusteringEnabled
            {
			   get
			   {
				 return isClusteringEnabled;
		    	}
                set
                {
                    isClusteringEnabled = value;
                    OnPropertyChanged(nameof(IsClusteringEnabled));
                }
            }

            /// <summary>
            /// Map region bound to <see cref="TKCustomMap"/>
            /// </summary>
            public MapSpan MapRegion
            {
                get { return mapRegion; }
                set
                {
                    if (mapRegion != value)
                    {
                        mapRegion = value;
                        OnPropertyChanged("MapRegion");
                    }
                }
            }

            /// <summary>
            /// Pins bound to the <see cref="TKCustomMap"/>
            /// </summary>
            public ObservableCollection<TKCustomMapPin> Pins
            {
                get { return pins; }
                set
                {
                    if (pins != value)
                    {
                        pins = value;
                        OnPropertyChanged("Pins");
                    }
                }
            }

            /// <summary>
            /// Routes bound to the <see cref="TKCustomMap"/>
            /// </summary>
            public ObservableCollection<TKRoute> Routes
            {
                get { return routes; }
                set
                {
                    if (routes != value)
                    {
                        routes = value;
                        OnPropertyChanged("Routes");
                    }
                }
            }

            /// <summary>
            /// Circles bound to the <see cref="TKCustomMap"/>
            /// </summary>
            public ObservableCollection<TKCircle> Circles
            {
                get { return circles; }
                set
                {
                    if (circles != value)
                    {
                        circles = value;
                        OnPropertyChanged("Circles");
                    }
                }
            }

            /// <summary>
            /// Lines bound to the <see cref="TKCustomMap"/>
            /// </summary>
            public ObservableCollection<TKPolyline> Lines
            {
                get { return lines; }
                set
                {
                    if (lines != value)
                    {
                        lines = value;
                        OnPropertyChanged("Lines");
                    }
                }
            }

            /// <summary>
            /// Polygons bound to the <see cref="TKCustomMap"/>
            /// </summary>
            public ObservableCollection<TKPolygon> Polygons
            {
                get { return polygons; }
                set
                {
                    if (polygons != value)
                    {
                        polygons = value;
                        OnPropertyChanged("Polygons");
                    }
                }
            }

            /// <summary>
            /// Map center bound to the <see cref="TKCustomMap"/>
            /// </summary>
            public Position MapCenter
            {
                get { return mapCenter; }
                set
                {
                    if (mapCenter != value)
                    {
                        mapCenter = value;
                        OnPropertyChanged("MapCenter");
                    }
                }
            }

            /// <summary>
            /// Selected pin bound to the <see cref="TKCustomMap"/>
            /// </summary>
            public TKCustomMapPin SelectedPin
            {
                get { return selectedPin; }
                set
                {
                    if (selectedPin != value)
                    {
                        selectedPin = value;
                        OnPropertyChanged("SelectedPin");
                    }
                }
            }


            /// <summary>
            /// Map Clicked bound to the <see cref="TKCustomMap"/>
            /// </summary>
            public Command<Position> MapClickedCommand
            {
                get
                {
                    return new Command<Position>((positon) =>
                    {
                        SelectedPin = null;

                        // Determine if a point was inside a circle
                        if ((from c in circles let distanceInMeters = c.Center.DistanceTo(positon) * 1000 where distanceInMeters <= c.Radius select c).Any())
                        {
                            UserDialogs.Instance.Alert("Circle tap", "Circle was tapped", "OK");
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
                            MapCenter = new Position(details.Item.Geometry.Location.Latitude, details.Item.Geometry.Location.Longitude);
                            return;
                        }
                        var osmResult = p as OsmNominatimResult;
                        if (osmResult != null)
                        {
                            MapCenter = new Position(osmResult.Latitude, osmResult.Longitude);
                            return;
                        }

                        if (Device.OS == TargetPlatform.Android)
                        {
                            var prediction = (TKNativeAndroidPlaceResult)p;

                            var details = await TKNativePlacesApi.Instance.GetDetails(prediction.PlaceId);

                            MapCenter = details.Coordinate;
                        }
                        else if (Device.OS == TargetPlatform.iOS)
                        {
                            var prediction = (TKNativeiOSPlaceResult)p;

                            MapCenter = prediction.Details.Coordinate;
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
                        MapRegion = MapSpan.FromCenterAndRadius(SelectedPin.Position, Distance.FromKilometers(1));
                    });
                }
            }
            public Command<IPlaceResult> DirectionsCommand
            {
                get
                {
                    return new Command<IPlaceResult>(async (p) =>
                    {
                        var location = db.GetDirections();
                        if (location == null) return;
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            this.fromPlace = new TKNativeiOSPlaceResult { Details = new TKPlaceDetails { Coordinate = new Position(location.A.Lat, location.A.Long) }, Description = location.A.Name };
                            this.from = new Position(location.A.Lat, location.A.Long);
                            this.toPlace = new TKNativeiOSPlaceResult { Details = new TKPlaceDetails { Coordinate = new Position(location.B.Lat, location.B.Long) }, Description = location.B.Name };
                            this.to = new Position(location.B.Lat, location.B.Long);
                        }
                        else
                        {
                            var t = new GeoLocationProvider().GetStreetAdd();
                            var test =await TKNativePlacesApi.Instance.GetPredictions(t, new MapSpan(new Position(-33.311836, 26.520642), -33.311836, 26.520642));
                            IPlaceResult first=null;
                            for (int i = 0; i < test.Count(); i++)
                            {
                                first = test.ElementAt(i);
                                break;
                            }
                            
                            
                        }
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
            /// Route clicked bound to the <see cref="TKCustomMap"/>
            /// </summary>
            public Command<TKRoute> RouteClickedCommand
            {
                get
                {
                    return new Command<TKRoute>(async r =>
                    {
                       await UserDialogs.Instance.AlertAsync("Clicked on route", "Route", "OK");
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
                            pins.Remove(pin);
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
                        pins.Clear();
                        circles.Clear();
                        if (routes != null)
                            routes.Clear();
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
                    var location = db.GetDirections();
                    return new Command(() =>
                    {
                        if (location == null) return;

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
                        MapRegion = r.Bounds;
                    });
                }
            }

            public Func<string, IEnumerable<TKCustomMapPin>, TKCustomMapPin> GetClusteredPin => (group, clusteredPins) =>
            {
                return null;
                //return new TKCustomMapPin
                //{
                //    DefaultPinColor = Color.Blue,
                //    Title = clusteredPins.Count().ToString(),
                //    ShowCallout = true
                //};
            };

            public TKCustomMapBindingsViewModel()
            {
                mapCenter = new Position(-33.311836, 26.520642);
                pins = new ObservableCollection<TKCustomMapPin>();
                circles = new ObservableCollection<TKCircle>();
                db = new RealmDataBase();
            }

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            public event PropertyChangedEventHandler PropertyChanged;

        }
 }