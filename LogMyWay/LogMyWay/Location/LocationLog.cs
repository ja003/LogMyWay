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
										//public List<Position> LoggedPositions = new List<Position>();

		public HashSet<Tuple<int, int>> loggedPositionsIndices;

		public LocationLog(string pName, Position pCenter, int pRadiusSteps)
		{
			Name = pName;
			Center = pCenter;
			RadiusSteps = pRadiusSteps;
			loggedPositionsIndices = new HashSet<Tuple<int, int>>();
		}

		public List<Position> GetLoggedPositions()
		{
			List<Position> positions = new List<Position>();
			foreach(Tuple<int, int> index in loggedPositionsIndices)
			{
				positions.Add(GetPositionOfLoggedIndex(index));
			}
			return positions;
		}

		/// <summary>
		/// Saves visited position.
		/// returns false if pPosition has already been visited (on smallest grid scale)
		/// </summary>
		public bool LogPosition(Position pPosition)
		{
			Tuple<int, int> index = GetIndexInGrid(pPosition, GridValues.GetStepSize(EGridStep.Small));

			bool alreadyLogged = loggedPositionsIndices.Contains(index);
			Debug.Log($"LogPosition {index}, logged = {alreadyLogged}");
			if(alreadyLogged)
				return false; //this area has been already visited => dont log it

			loggedPositionsIndices.Add(index);
			return true;
		}

		/// <summary>
		/// Returns area [topLeft, botRight] on grid containing given position with given stepSize
		/// </summary>
		public GridArea GetAreaOnPosition(Position position, double pGridStepSize)
		{
			return GetAreaOnIndex(GetIndexInGrid(position, pGridStepSize), pGridStepSize);
		}

		private Position GetPositionOfLoggedIndex(Tuple<int, int> pIndex)
		{
			int y = pIndex.Item2 > 0 ? -1 : 1;
			int x = pIndex.Item1 < 0 ? 1 : -1;
			double pGridStepSize = GridValues.GetStepSize(EGridStep.Small);

			Position pos = new Position(
				Center.Latitude + (pIndex.Item2 + y) * pGridStepSize,
				Center.Longitude + (pIndex.Item1 + x) * pGridStepSize);

			return pos;
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
		public Tuple<int, int> GetIndexInGrid(Position position, double pGridStepSize)
		{
			//central coordinate system doesnt contain 0 index

			double xDouble = (position.Longitude - Center.Longitude) / pGridStepSize;
			int xSign = Math.Sign(xDouble);
			int x = (int)Math.Ceiling(Math.Abs(xDouble)) * xSign;

			double yDouble = (position.Latitude - Center.Latitude) / pGridStepSize;
			int ySign = Math.Sign(yDouble);
			int y = (int)Math.Ceiling(Math.Abs(yDouble)) * ySign;


			System.Diagnostics.Debug.Write($"@@@@@ GetIndexInGrid {position.Latitude},{position.Longitude} = [{x},{y}]");

			return new Tuple<int, int>(x, y);
		}
	}
}
