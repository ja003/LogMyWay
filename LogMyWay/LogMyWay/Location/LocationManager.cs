using LogMyWay.Data;
using System.Collections.Generic;
using System.Linq;
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

		public static List<LocationLog> Locations = new List<LocationLog>();
		public static LocationLog CurrentLocation;

		/// <summary>
		/// Moves map center to current position after obtaining it
		/// </summary>
		public static async void MoveToCurrentPosition()
		{
			Position currentPosition = await GpsManager.GetCurrentPosition();

			map.MoveToRegion(MapSpan.FromCenterAndRadius(currentPosition, Distance.FromMiles(1)));

			App.Current.MapPage.OnStart();
		}

		public static async void LoadSavedLocations()
		{
			//Debug.Log("LoadSavedLocations");
			List<string> savedLocationsNames = await DataManager.GetSavedLocationsNames();

			bool noLocationSaved = savedLocationsNames == null || savedLocationsNames.Count == 0;
			if(noLocationSaved)
			{
				Debug.Log("no locations yet");
				//SetCreateLocation(true); //user creates himself
				return;
			}

			//Debug.Log($"#locations = {savedLocationsNames.Count}");
			foreach(string locationName in savedLocationsNames)
			{
				//Debug.Log($"{savedLocationsNames.IndexOf(locationName)} = {locationName}");
				Locations.Add(await DataManager.LoadLocation(locationName));
			}
			App.Current.MapPage.OnLocationsLoaded();

			//EGridStep lastgridStep = App.Current.LastGridStep;

			//set last active location
			string lastLocation = App.Current.LastActiveLocation;
			if(lastLocation == null)
			{
				Debug.Log($"activeLocation is null");
				return;
			}

			SetActiveLocation(lastLocation);
		}

		public static void SetActiveLocation(string pLocationName)
		{
			LocationLog location = Locations.First(a => a.Name == pLocationName);
			if(location == null)
			{
				Debug.Log($"location {pLocationName} not found");
				return;
			}

			CurrentLocation = location;
			App.Current.LastActiveLocation = pLocationName;

			//draw
			Debug.Log($"SetActiveLocation {location.Name}");
			map.Renderer.DrawLocation(location);
			App.Current.MapPage.OnLocationSet();
		}

		/*private static void SetCreateLocation(bool pActive)
		{
			Debug.Log($"SetCreateLocation {pActive}");
			App.Current.MapPage.IsCreateActive = pActive;
		}*/

		public static async void CreateLocation(string pName, int pRadius)
		{
			Debug.Log($"CreateLocation {pName}");
			LocationLog newLocation = new LocationLog(pName, map.VisibleRegion.Center, pRadius);
			bool saveResult = await DataManager.SaveLocation(newLocation);
			Debug.Log($"saveResult {saveResult}");
			//todo: handle save fail
			Locations.Add(newLocation);
			SetActiveLocation(newLocation.Name);
			//SetCreateLocation(false);
		}

		public static async void LogPosition(Position pPosition)
		{
			if(CurrentLocation == null)
			{
				Debug.Log("Error: CurrentLocation null");
				return;
			}

			bool logged = CurrentLocation.LogPosition(pPosition);

			if(logged)
				map.Renderer.DrawLoggedPosition(pPosition);
			await DataManager.SaveLocation(CurrentLocation);
		}

		//public static async void CreateLocation(object sender, EventArgs e)
		//{
		//	//todo: get name from eventArgs
		//	string name = App.Current.MapPage.NewLocationName;
		//	Debug.Log($"CreateLocation {name}");
		//	LocationLog newLocation = new LocationLog(name, map.VisibleRegion.Center);
		//	bool saveResult = await DataManager.SaveLocation(newLocation);
		//	Debug.Log($"saveResult {saveResult}");
		//	//todo: handle save fail
		//	locations.Add(newLocation);
		//	SetActiveLocation(newLocation);
		//	SetCreateLocation(false);
		//}
	}
}
