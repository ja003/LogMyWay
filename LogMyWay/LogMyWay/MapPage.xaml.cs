using LogMyWay.Data;
using System;
using System.Collections.Generic;
using LogMyWay.Location;
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

			btnCreateLocation.Clicked += LocationManager.CreateLocation;
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
				OnPropertyChanged(nameof(DebugText)); 
			}
		}

		private bool isCreateBtnVisible;
		public bool IsCreateBtnVisible
		{
			get => isCreateBtnVisible;
			set
			{
				isCreateBtnVisible = value;
				OnPropertyChanged(nameof(IsCreateBtnVisible)); 
			}
		}

		public string NewLocationName => textNewLocationName.Text;

		public void OnStart()
		{
			Debug.Log("OnStart");
			LocationManager.LoadSavedLocations();
		}

		
	}
}