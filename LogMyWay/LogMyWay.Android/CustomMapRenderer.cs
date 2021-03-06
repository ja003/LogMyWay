﻿using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using LogMyWay;
using LogMyWay.Droid;
using LogMyWay.Location;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace LogMyWay.Droid
{
	/// <summary>
	/// Visual effects added to the map
	/// </summary>
	public class CustomMapRenderer : MapRenderer, ICustomMapRenderer
	{

		//private CustomMap customMap;

		public CustomMapRenderer(Context context) : base(context)
		{
			//MessagingCenter.Subscribe<CustomMap>(this, "LogPosition", OnLogPosition);
			//MessagingCenter.Subscribe<CustomMap, Position>(this, "LogPosition", (source, arg) => { OnLogPosition(arg); });
			App.Current.Map.Renderer = this;
		}


		///OVERRIDES
		private bool onMapReadyCalled;

		protected override void OnMapReady(GoogleMap googleMap)
		{
			base.OnMapReady(googleMap);

			if(onMapReadyCalled)
				return;
			onMapReadyCalled = true;

			//move to current location only at first call
			LocationManager.MoveToCurrentPosition();
		}

		/// <summary>
		/// We override the OnElementChanged() event handler to get the desired instance. We also use it for updates.
		/// </summary>
		/// <param name="e">It contains either the NewElement or the OldElement</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
		{
			base.OnElementChanged(e);

			//todo: find out when it is called...does it have to be called always?
			if(e.NewElement != null)
			{
				//customMap = e.NewElement as CustomMap;
				Control.GetMapAsync(this);
			}
		}


		public void OnSetGridStep(bool pRedraw)
		{
			if(pRedraw)
				DrawLocation(LocationManager.CurrentLocation);
		}

		/*private void RedrawCurrentLocation()
		{
			NativeMap.Clear();
			DrawLocation(LocationManager.CurrentLocation);
		}*/

		/// <summary>
		/// Clear current map.
		/// Draw location grid, center and all logged positions with given grid step size
		/// </summary>
		public void DrawLocation(LocationLog pLocation)
		{
			NativeMap.Clear();
			drawnIndices.Clear();

			LogMyWay.Debug.Log($"DrawLocation {pLocation.Name}, " +
				$"center = {pLocation.Center.Latitude.ToString("0.00")},{pLocation.Center.Longitude.ToString("0.00")}");

			NativeMap.Clear();
			List<PolylineOptions> lines = ShapeGenerator.GetLines(pLocation, App.Current.LastGridStep);
			foreach(PolylineOptions line in lines)
			{
				NativeMap.AddPolyline(line);
			}

			CircleOptions gridCenterCircle = ShapeGenerator.GetCircle(pLocation.Center);
			NativeMap.AddCircle(gridCenterCircle);

			//todo: draw all logged positions
			foreach(Position position in pLocation.GetLoggedPositions())
			{
				DrawLoggedPosition(position);
			}
		}

		private HashSet<Tuple<int, int>> drawnIndices = new HashSet<Tuple<int, int>>();

		public void DrawLoggedPosition(Position pPosition)
		{
			Tuple<int, int> index = LocationManager.CurrentLocation.GetIndexInGrid(
				pPosition, GridValues.GetStepSize(App.Current.LastGridStep));

			LogMyWay.Debug.Log($"DrawLoggedPosition {index}");
			if(drawnIndices.Contains(index))
				return;
			LogMyWay.Debug.Log($"- ok");

			drawnIndices.Add(index);

			PolygonOptions polygon = ShapeGenerator.GetPolygonOnPosition(
				LocationManager.CurrentLocation, pPosition, App.Current.LastGridStep);
			NativeMap.AddPolygon(polygon);
		}

		public void DrawDebugPosition(Position pPosition)
		{
			CircleOptions gridCenterCircle = ShapeGenerator.GetCircle(pPosition, 25);			
			NativeMap.AddCircle(gridCenterCircle);
		}
	}
}