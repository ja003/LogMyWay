using LogMyWay.Data;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

using Position = Xamarin.Forms.Maps.Position;

namespace LogMyWay.Location
{
	/// <summary>
	/// GPS related methods
	/// </summary>
	public static class GpsManager
	{
		public static async Task<Position> GetCurrentPosition()
		{
			Plugin.Geolocator.Abstractions.Position position = await RequestCurrentPosition();

			return new Position(position.Latitude, position.Longitude);
		}

		private static async Task<Plugin.Geolocator.Abstractions.Position> RequestCurrentPosition()
		{
			Plugin.Geolocator.Abstractions.Position position = null;
			try
			{
				var locator = Plugin.Geolocator.CrossGeolocator.Current;
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


		private static IGeolocator locator => CrossGeolocator.Current;

		public static async void Init()
		{
			await StartListening();
		}

		private static async void OnPositionError(object sender, PositionErrorEventArgs e)
		{
			Debug.Log($"OnPositionError");

			await locator.StopListeningAsync();

			await StartListening();
		}

		/*private static async Task<bool> StartListening()
		{
			Debug.Log($"StartListening");
			await locator.StartListeningAsync(new TimeSpan(0, 0, 5), 1); //min 5 secs between updates, min 1 meter difference
			return true;
		}*/

		private static async Task StartListening()
		{
			if(CrossGeolocator.Current.IsListening)
				return;

			//min 5 secs between updates, min 1 meter difference
			await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 1, true);

			CrossGeolocator.Current.PositionChanged += OnPositionChanged;
			CrossGeolocator.Current.PositionError += OnPositionError;
		}

		private static async Task StopListening()
		{
			if(!CrossGeolocator.Current.IsListening)
				return;

			await CrossGeolocator.Current.StopListeningAsync();

			CrossGeolocator.Current.PositionChanged -= OnPositionChanged;
			CrossGeolocator.Current.PositionError -= OnPositionError;
		}

		private static void OnPositionChanged(object sender, PositionEventArgs e)
		{
			Plugin.Geolocator.Abstractions.Position position = e.Position;
			Position newPosition = DataConvertor.GetPosition(position);
			Debug.Log($"OnPositionChanged {DataConvertor.PositionString(newPosition)}");
			LocationManager.LogPosition(newPosition);
		}
	}
}