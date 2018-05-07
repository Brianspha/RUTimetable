using System;
using TK.CustomMap;
using Xamarin.Forms;

namespace RUTimetable
{
	public class TKCustomMapViewModel
	{
		TKCustomMap mapView;
		public TKCustomMapViewModel()
		{
			SetUp();
		}
		public void SetUp()
		{
		 	mapView = new TKCustomMap(MapSpan.FromCenterAndRadius(new Position(-33.311836, 26.520642), Distance.FromKilometers(2)));
			mapView.SetBinding(TKCustomMap.IsClusteringEnabledProperty, "IsClusteringEnabled");
            mapView.SetBinding(TKCustomMap.GetClusteredPinProperty, "GetClusteredPin");
            mapView.SetBinding(TKCustomMap.PinsProperty, "Pins");
            mapView.SetBinding(TKCustomMap.PinSelectedCommandProperty, "PinSelectedCommand");
            mapView.SetBinding(TKCustomMap.SelectedPinProperty, "SelectedPin");
            mapView.SetBinding(TKCustomMap.RoutesProperty, "Routes");
            mapView.SetBinding(TKCustomMap.CirclesProperty, "Circles");
            mapView.SetBinding(TKCustomMap.CalloutClickedCommandProperty, "CalloutClickedCommand");
            mapView.SetBinding(TKCustomMap.PolylinesProperty, "Lines");
            mapView.SetBinding(TKCustomMap.PolygonsProperty, "Polygons");
            mapView.SetBinding(TKCustomMap.MapRegionProperty, "MapRegion");
            mapView.SetBinding(TKCustomMap.RouteClickedCommandProperty, "RouteClickedCommand");
            mapView.SetBinding(TKCustomMap.RouteCalculationFinishedCommandProperty, "RouteCalculationFinishedCommand");
            mapView.SetBinding(TKCustomMap.MapFunctionsProperty, "MapFunctions");
            mapView.IsRegionChangeAnimated = true;
            mapView.IsShowingUser = true;

		}
		public TKCustomMap GetMap()
		{
			return mapView;
		}
	}
}
