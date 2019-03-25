using Android.Gms.Maps.Model;
using LogMyWay.Location;
using LogMyWay.Structures;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace LogMyWay.Droid
{
	public static class ShapeGenerator
	{

		public static List<PolylineOptions> GetLines(LocationLog pLocation, EGridStep pStep)
		{
			Position center = pLocation.Center;
			Position topLeft = GridValues.GetLocationCorner(pLocation, true);
			Position botRight = GridValues.GetLocationCorner(pLocation, false);

			double latitude = center.Latitude;
			double longitude = center.Longitude;

			List<GridLine> lines = new List<GridLine>();
			int steps = GridValues.GetStepsCount(pLocation.RadiusSteps, pStep);
			double stepSize = GridValues.GetStepSize(pStep);

			//center<inc> to top
			for(int y = 0; y <= steps; y++)
			{
				latitude = center.Latitude + y * stepSize;

				lines.Add(new GridLine(
					 new Position(latitude, topLeft.Longitude),
					 new Position(latitude, botRight.Longitude)));
			}
			//center<exc> to bot
			for(int y = 1; y <= steps; y++)
			{
				latitude = center.Latitude - y * stepSize;

				lines.Add(new GridLine(
					 new Position(latitude, topLeft.Longitude),
					 new Position(latitude, botRight.Longitude)));
			}

			//center<inc> to left
			for(int x = 0; x <= steps; x++)
			{
				longitude = center.Longitude - x * stepSize;

				lines.Add(new GridLine(
					 new Position(topLeft.Latitude, longitude),
					 new Position(botRight.Latitude, longitude)));
			}
			//center<exc> to right
			for(int x = 1; x <= steps; x++)
			{
				longitude = center.Longitude + x * stepSize;

				lines.Add(new GridLine(
					 new Position(topLeft.Latitude, longitude),
					 new Position(botRight.Latitude, longitude)));
			}

			List<PolylineOptions> polyLines = new List<PolylineOptions>();
			foreach(GridLine line in lines)
			{
				PolylineOptions lineOptions = GetLine();

				lineOptions.Add(new LatLng(line.Start.Latitude, line.Start.Longitude));
				lineOptions.Add(new LatLng(line.End.Latitude, line.End.Longitude));

				polyLines.Add(lineOptions);
			}

			return polyLines;
		}
				

		private static PolylineOptions GetLine()
		{
			PolylineOptions lineOptions = new PolylineOptions();
			lineOptions.InvokeColor(0x660000FF);
			lineOptions.InvokeWidth(5);
			return lineOptions;
		}
				

		public static CircleOptions GetCircle(Position pCenter, int pRadius = 50)
		{
			CircleOptions centerCircle = new CircleOptions();
			centerCircle.InvokeCenter(new LatLng(pCenter.Latitude, pCenter.Longitude));
			centerCircle.InvokeRadius(pRadius);
			centerCircle.InvokeFillColor(Android.Graphics.Color.DarkSlateBlue);
			centerCircle.InvokeStrokeColor(0X66FF0000);
			centerCircle.InvokeStrokeWidth(2);
			return centerCircle;
		}

		/// <summary>
		/// Creates polygon on given location's grid containing given position with given stepSize
		/// </summary>
		public static PolygonOptions GetPolygonOnPosition(LocationLog pLocation, Position pPosition, EGridStep pStep)
		{
			GridArea area = pLocation.GetAreaOnPosition(pPosition, GridValues.GetStepSize(pStep));

			PolygonOptions polygon = new PolygonOptions();
			polygon.InvokeFillColor(0x66FF0000);
			polygon.InvokeStrokeColor(0x660000FF);
			polygon.InvokeStrokeWidth(10.0f);

			polygon.Add(new LatLng(area.TopLeft.Latitude, area.TopLeft.Longitude));
			polygon.Add(new LatLng(area.BotRight.Latitude, area.TopLeft.Longitude));
			polygon.Add(new LatLng(area.BotRight.Latitude, area.BotRight.Longitude));
			polygon.Add(new LatLng(area.TopLeft.Latitude, area.BotRight.Longitude));

			return polygon;
		}

		//private static GridArea GetAreaOnPosition(LocationLog pLocation, Position pPosition, EGridStep pStep)
		//{
		//	return pLocation.GetAreaOnPosition(pPosition, GetStepSize(pStep));
		//}			
	}
}