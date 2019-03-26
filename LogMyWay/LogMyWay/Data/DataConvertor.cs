using Xamarin.Forms.Maps;

namespace LogMyWay.Data
{
	public static class DataConvertor
	{
		public static string PositionString(Position pPos)
		{
			return $"[{pPos.Latitude.ToString("0.0")}_{pPos.Longitude.ToString("0.0")}]";
		}

		public static Position GetPosition(Plugin.Geolocator.Abstractions.Position pPos)
		{
			return new Position(pPos.Latitude, pPos.Longitude);
		}
	}
}
