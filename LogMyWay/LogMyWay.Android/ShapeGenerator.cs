using Android.Gms.Maps.Model;
using LogMyWay.Location;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace LogMyWay.Droid
{
	public static class ShapeGenerator
	{
		public const double MIN_GRID_STEP = 0.001f;

		public static List<PolylineOptions> GetLines(LocationLog pLocation, EGridStep pStep)
		{
			Position topLeft = new Position(pLocation.Center.Latitude + pLocation.RadiusSteps * MIN_GRID_STEP, pLocation.Center.Longitude - pLocation.RadiusSteps * MIN_GRID_STEP);

			double latitude = topLeft.Latitude;
			double longitude = topLeft.Longitude;

			List<GridLine> lines = new List<GridLine>();
			//left to right
			int steps = GetStepsCount(pLocation.RadiusSteps, pStep);
			double stepSize = GetStepSize(pStep);

			for(int y = 0; y < 2 * steps; y++)
			{
				latitude = topLeft.Latitude + y * stepSize;

				lines.Add(new GridLine(new Position(latitude, longitude), new Position(latitude, longitude + 2 * steps * stepSize)));
			}

			List<PolylineOptions> polyLines = new List<PolylineOptions>();
			foreach (GridLine line in lines)
			{
				PolylineOptions lineOptions = GetLine();

				lineOptions.Add(new LatLng(line.Start.Latitude, line.Start.Longitude));
				lineOptions.Add(new LatLng(line.End.Latitude, line.End.Longitude));

				polyLines.Add(lineOptions);
			}

			return polyLines;
		}

		private static double GetStepSize(EGridStep pStep)
		{
			int multiply = 1;
			switch(pStep)
			{
				case EGridStep.Small:
					multiply = 1;
					break;
				case EGridStep.Medium:
					multiply = 2;
					break;
				case EGridStep.Large:
					multiply = 3;
					break;
			}
			return MIN_GRID_STEP * multiply;
		}

		private static PolylineOptions GetLine()
		{
			PolylineOptions lineOptions = new PolylineOptions();
			lineOptions.InvokeColor(0x660000FF);
			lineOptions.InvokeWidth(5);
			return lineOptions;
		}

		private static int GetStepsCount(int pLocationRadiusSteps, EGridStep pStep)
		{
			return (int)(pLocationRadiusSteps / GetStepSize(pStep));
		}

		public static CircleOptions GetCenter(LocationLog pLocation)
		{
			CircleOptions centerCircle = new CircleOptions();
			centerCircle.InvokeCenter(new LatLng(pLocation.Center.Latitude, pLocation.Center.Longitude));
			centerCircle.InvokeRadius(50);
			centerCircle.InvokeFillColor(Android.Graphics.Color.DarkSlateBlue);
			centerCircle.InvokeStrokeColor(0X66FF0000);
			centerCircle.InvokeStrokeWidth(2);
			return centerCircle;
		}

		private struct GridLine
		{
			public Position Start { get; private set; }
			public Position End { get; private set; }

			public GridLine(Position pStart, Position pEnd) : this()
			{
				this.Start = pStart;
				this.End = pEnd;
			}
		}
	}
}