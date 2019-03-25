using LogMyWay.Structures;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace LogMyWay.Location
{
	public class LocationLog
	{
		public string Name;
		public Position Center;
		public int RadiusSteps; //number of minimum size steps between center and border
		public List<Position> LoggedPositions = new List<Position>();

		public LocationLog(string pName, Position pCenter, int pRadiusSteps)
		{
			Name = pName;
			Center = pCenter;
			RadiusSteps = pRadiusSteps;
		}

		/// <summary>
		/// Saves visited position.
		/// returns false if pPosition has already been visited (on smallest grid scale)
		/// </summary>
		public bool LogPosition(Position pPosition)
		{
			if(LoggedPositions == null)
			{
				LoggedPositions = new List<Position>();
			}
			LoggedPositions.Add(pPosition);

			//todo: check if visited
			return true;
		}

		/// <summary>
		/// Returns area [topLeft, botRight] on grid containing given position with given stepSize
		/// </summary>
		public GridArea GetAreaOnPosition(Position position, double pGridStepSize)
		{
			return GetAreaOnIndex(GetIndexInGrid(position, pGridStepSize), pGridStepSize);
		}

		private GridArea GetAreaOnIndex(Tuple<int, int> pIndex, double pGridStepSize)
		{
			int y = pIndex.Item2 > 0 ? pIndex.Item2 : pIndex.Item2 + 1;
			int x = pIndex.Item1 < 0 ? pIndex.Item1 : pIndex.Item1 - 1;
			Position topLeft = new Position(Center.Latitude + y * pGridStepSize, Center.Longitude + x * pGridStepSize);
			Position botRight = new Position(topLeft.Latitude - pGridStepSize, topLeft.Longitude + pGridStepSize);
			//Position botRight = new Position(GridCenter.Latitude + (index.Item2) * GridStepSize, GridCenter.Longitude + (index.Item1 - 1) * GridStepSize);

			return new GridArea(topLeft, botRight);
		}

		/// <summary>
		/// Item1 = x = longitude
		/// Item2 = y = latitude
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		private Tuple<int, int> GetIndexInGrid(Position position, double pGridStepSize)
		{
			//central coordinate system doesnt contain 0 index

			double xDouble = (position.Longitude - Center.Longitude) / pGridStepSize;
			int xSign = Math.Sign(xDouble);
			int x = (int)Math.Ceiling(Math.Abs(xDouble)) * xSign;

			double yDouble = (position.Latitude - Center.Latitude) / pGridStepSize;
			int ySign = Math.Sign(yDouble);
			int y = (int)Math.Ceiling(Math.Abs(yDouble)) * ySign;


			//if(Math.Abs(xDouble) < 1)
			//{
			//	 xDouble += Math.Sign(xDouble);
			//}
			//int x = (int)xDouble;

			//double yDouble = (position.Latitude - GridCenter.Latitude) / GridStepSize;
			//if(Math.Abs(yDouble) < 1)
			//{
			//	 yDouble += Math.Sign(yDouble);
			//}
			//int y = (int)yDouble;

			System.Diagnostics.Debug.Write($"@@@@@ GetIndexInGrid {position.Latitude},{position.Longitude} = [{x},{y}]");

			return new Tuple<int, int>(x, y);
		}
	}
}
