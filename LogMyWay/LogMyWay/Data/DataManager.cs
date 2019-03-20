using LogMyWay.Location;
using Newtonsoft.Json;
using PCLStorage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogMyWay.Data
{
	public static class DataManager
	{
		public static async Task<List<string>> GetSavedLocationsNames()
		{
			IFolder folder = FileSystem.Current.LocalStorage;
			IList<IFile> allFilesInFolder = await folder.GetFilesAsync();
			if(allFilesInFolder == null)
				return null;

			List<string> names = new List<string>();
			foreach(IFile f in allFilesInFolder)
			{
				names.Add(f.Name);
			}

			return names;
		}

		public static async void SaveLocation(LocationLog pLocation)
		{
			string serializedLocation = JsonConvert.SerializeObject(pLocation, Formatting.Indented);
			await pLocation.Name.WriteTextAllAsync(serializedLocation);
		}

		public static async Task<LocationLog> LoadLocation(string pLocationName)
		{
			string serializedLocation = await pLocationName.ReadAllTextAsync();
			LocationLog savedLocation = JsonConvert.DeserializeObject<LocationLog>(serializedLocation);

			return savedLocation;
		}
	}
}