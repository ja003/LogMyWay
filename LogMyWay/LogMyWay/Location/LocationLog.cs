using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace LogMyWay.Location
{
	public class LocationLog
	{
		public string Name;
		public Position Center;
		public List<Position> LoggedPositions = new List<Position>();

		public LocationLog(string name, Position center)
		{
			Name = name;
			Center = center;
		}

		public void LogPosition(Position position)
		{
			if(LoggedPositions == null)
			{
				LoggedPositions = new List<Position>();
			}
			LoggedPositions.Add(position);
		}
	}
}
