using LogMyWay.Location;
using Newtonsoft.Json;
using PCLStorage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogMyWay.Data
{
	/// <summary>
	/// Save/Load to all data structures - LocationLogs
	/// - each Location info is stored in seperate file
	/// </summary>
	public static class DataManager
	{
		private const string LOCATIONS_FOLDER_NAME = "locations";

		public static async Task<List<string>> GetSavedLocationsNames()
		{
			IFolder folder = await GetLocationSaveFolder();

			IList<IFile> allFilesInFolder = await folder.GetFilesAsync();

			//return all file names in list
			return allFilesInFolder?.Select(f => f.Name).ToList();
		}

		/// <summary>
		/// Create or update location file
		/// </summary>
		public static async Task<bool> SaveLocation(LocationLog pLocation)
		{
			string serializedLocation = JsonConvert.SerializeObject(pLocation, Formatting.Indented);
			//todo: check if fail -> return false
			await pLocation.Name.WriteTextAllAsync(serializedLocation, await GetLocationSaveFolder());
			return true;
		}

		public static async Task<LocationLog> LoadLocation(string pLocationName)
		{
			IFolder folder = await GetLocationSaveFolder();
			string serializedLocation = await pLocationName.ReadAllTextAsync(folder);
			LocationLog savedLocation = JsonConvert.DeserializeObject<LocationLog>(serializedLocation);

			return savedLocation;
		}

		/// <summary>
		/// Returns folder containing all location files.
		/// If not exists -> creates it
		/// </summary>
		/// <returns></returns>
		private static async Task<IFolder> GetLocationSaveFolder()
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;

			bool isFolderExist = await LOCATIONS_FOLDER_NAME.IsFolderExistAsync();

			IFolder locationFolder = isFolderExist
				? await rootFolder.GetFolderAsync(LOCATIONS_FOLDER_NAME)
				: await LOCATIONS_FOLDER_NAME.CreateFolder(rootFolder);

			return locationFolder;
		}
	}
}