using System.Collections.Generic;
using System.Threading.Tasks;
using LogMyWay.Data;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace LogMyWay.Location
{
	/// <summary>
	/// Controls all map operations
	/// </summary>
	public static class LocationManager
	{
		private static CustomMap map => ((App)Application.Current).Map;

		/// <summary>
		/// Moves map center to current position after obtaining it
		/// </summary>
		public static async void MoveToCurrentPosition()
		{
			Position currentPosition = await GpsManager.GetCurrentPosition();

			map.MoveToRegion(MapSpan.FromCenterAndRadius(currentPosition, Distance.FromMiles(1)));

			App.Current.MapPage.OnStart();
		}
	}
}
