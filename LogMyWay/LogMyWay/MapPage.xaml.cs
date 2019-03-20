using LogMyWay.Data;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LogMyWay
{
	/// <summary>
	/// Main app page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage()
		{
			InitializeComponent();
			BindingContext = this;
			//Debug.Log("Init"); //cant debug yet
		}

		public CustomMap GetMap()
		{
			return customMap;
		}


		private string debugText = "_";
		public string DebugText
		{
			get => debugText;
			set
			{
				debugText = value;
				OnPropertyChanged(nameof(DebugText)); // Notify that there was a change on this property
			}
		}

		//public void Debug(string pText)
		//{
		//	DebugText += Environment.NewLine + pText;
		//	//debugText.Text += Environment.NewLine + pText;
		//}

		public void OnStart()
		{
			Debug.Log("OnStart");
			LoadSavedLocations();
		}

		private async void LoadSavedLocations()
		{
			List<string> savedLocationsNames = await DataManager.GetSavedLocationsNames();

			if(savedLocationsNames == null)
			{
				Debug.Log("no locations yet");
			}
			else
			{
				Debug.Log($"#locations = {savedLocationsNames.Count}");
				foreach(string location in savedLocationsNames)
				{
					Debug.Log($"{savedLocationsNames.IndexOf(location)} = {location}");
				}
			}
		}
	}
}