using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using LogMyWay;
using LogMyWay.Droid;
using LogMyWay.Location;
using LogMyWay.Structures;
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

		/// <summary>
		/// Draw location grid, center and all logged positions with given grid step size
		/// </summary>
		public void DrawLocation(LocationLog pLocation)
		{
			LogMyWay.Debug.Log($"DrawLocation {pLocation.Name}, " +
				$"center = {pLocation.Center.Latitude.ToString("0.00")},{pLocation.Center.Longitude.ToString("0.00")}");

			NativeMap.Clear();
			List<PolylineOptions> lines = ShapeGenerator.GetLines(pLocation, App.Current.Map.CurrentGridStep);
			foreach(PolylineOptions line in lines)
			{
				NativeMap.AddPolyline(line);
			}

			CircleOptions gridCenterCircle = ShapeGenerator.GetCenterCircle(pLocation);
			NativeMap.AddCircle(gridCenterCircle);

			//todo: draw all logged positions
			foreach(Position position in pLocation.LoggedPositions)
			{
				DrawLoggedPosition(position);
			}
		}

		public void DrawLoggedPosition(Position pPosition)
		{
			PolygonOptions polygon = ShapeGenerator.GetPolygonOnPosition(
				LocationManager.ActiveLocation, pPosition, App.Current.Map.CurrentGridStep);
			NativeMap.AddPolygon(polygon);
		}
	}
}