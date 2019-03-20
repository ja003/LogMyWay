using System;
using System.Collections.Generic;
using LogMyWay.Data;
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
			Debug("Init");
			Debug("Init");
			Debug("Init");
			Debug("Init");
			Debug("Init");
			Debug("Init");
			Debug("Init");
			Debug("Init");
		}

		public CustomMap GetMap()
		{
			return customMap;
		}


		private string debugText;
		public string DebugText
		{
			get => debugText;
			set
			{
				debugText = value;
				OnPropertyChanged(nameof(DebugText)); // Notify that there was a change on this property
			}
		}

		public void Debug(string pText)
		{
			DebugText += Environment.NewLine + pText;
			//debugText.Text += Environment.NewLine + pText;
		}

		public void OnStart()
		{
			Debug("OnStart");
			LoadSavedLocations();
		}

		private async void LoadSavedLocations()
		{
			List<string> savedLocationsNames = await DataManager.GetSavedLocationsNames();

			if(savedLocationsNames == null)
			{
				App.Current.MapPage.Debug("no locations yet");
			}
			else
			{
				foreach(string location in savedLocationsNames)
				{
					App.Current.MapPage.Debug($"{savedLocationsNames.IndexOf(location)} = {location}");
				}
			}
		}
	}
}