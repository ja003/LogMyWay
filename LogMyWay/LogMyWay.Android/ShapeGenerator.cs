using Android.Gms.Maps.Model;
using LogMyWay.Location;
using LogMyWay.Structures;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace LogMyWay.Droid
{
	public static class ShapeGenerator
	{
		public const double MIN_GRID_STEP = 0.001f;

		public static List<PolylineOptions> GetLines(LocationLog pLocation, EGridStep pStep)
		{
			Position center = pLocation.Center;
			Position topLeft = new Position(
				center.Latitude + pLocation.RadiusSteps * MIN_GRID_STEP,
				center.Longitude - pLocation.RadiusSteps * MIN_GRID_STEP);
			Position botRight = new Position(
				center.Latitude - pLocation.RadiusSteps * MIN_GRID_STEP,
				center.Longitude + pLocation.RadiusSteps * MIN_GRID_STEP);

			double latitude = center.Latitude;
			double longitude = center.Longitude;

			List<GridLine> lines = new List<GridLine>();
			int steps = GetStepsCount(pLocation.RadiusSteps, pStep);
			double stepSize = GetStepSize(pStep);

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

		private static int GetRadiusStep(EGridStep pStep)
		{
			int radiusStep = 1;
			switch(pStep)
			{
				case EGridStep.Small:
					radiusStep = 1;
					break;
				case EGridStep.Medium:
					radiusStep = 2;
					break;
				case EGridStep.Large:
					radiusStep = 3;
					break;
			}
			return radiusStep;
		}

		private static double GetStepSize(EGridStep pStep)
		{
			return MIN_GRID_STEP * GetRadiusStep(pStep);
		}

		private static PolylineOptions GetLine()
		{
			PolylineOptions lineOptions = new PolylineOptions();
			lineOptions.InvokeColor(0x660000FF);
			lineOptions.InvokeWidth(5);
			return lineOptions;
		}

		/// <summary>
		/// Conversion of radius steps based on GridStep. 
		/// EGridStep.Small => returns pLocationRadiusSteps
		/// </summary>
		private static int GetStepsCount(int pLocationRadiusSteps, EGridStep pStep)
		{
			return pLocationRadiusSteps / GetRadiusStep(pStep);
		}

		public static CircleOptions GetCenterCircle(LocationLog pLocation)
		{
			CircleOptions centerCircle = new CircleOptions();
			centerCircle.InvokeCenter(new LatLng(pLocation.Center.Latitude, pLocation.Center.Longitude));
			centerCircle.InvokeRadius(50);
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
			GridArea area = pLocation.GetAreaOnPosition(pPosition, GetStepSize(pStep));

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