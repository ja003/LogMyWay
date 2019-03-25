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

		public void LogPosition(Position pPosition)
		{
			if(LoggedPositions == null)
			{
				LoggedPositions = new List<Position>();
			}
			LoggedPositions.Add(pPosition);
		}
	}
}
