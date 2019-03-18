using System;
using System.Threading.Tasks;
using LogMyWay.Location;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace LogMyWay.Droid
{
	 public static class GpsManager 
	 {
		  public static async Task<Position> GetCurrentPosition()
		  {
				Plugin.Geolocator.Abstractions.IGeolocator locator = CrossGeolocator.Current;
				Plugin.Geolocator.Abstractions.Position position = await RequestCurrentPosition();

				return new Position(position.Latitude, position.Longitude);
		  }

		  private static async Task<Plugin.Geolocator.Abstractions.Position> RequestCurrentPosition()
		  {
				Plugin.Geolocator.Abstractions.Position position = null;
				try
				{
					 var locator = CrossGeolocator.Current;
					 locator.DesiredAccuracy = 100;

					 position = await locator.GetLastKnownLocationAsync();

					 if(position != null)
					 {
						  //got a cahched position, so let's use it.
						  return position;
					 }

					 if(!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
					 {
						  //not available or enabled
						  return null;
					 }

					 position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

				}
				catch(Exception ex)
				{
					 //Debug.WriteLine("Unable to get location: " + ex);
				}

				if(position == null)
					 return null;

				var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
						position.Timestamp, position.Latitude, position.Longitude,
						position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

				//Debug.WriteLine(output);

				return position;
		  }
	 }
}