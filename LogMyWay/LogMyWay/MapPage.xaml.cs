using LogMyWay.Location;
using System;
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
		#region Properties

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

		private const string TEXT_CREATE = "+";
		private const string TEXT_CONFIRM = "OK";

		private string createBtnText = TEXT_CREATE;
		public string CreateBtnText
		{
			get => createBtnText;
			set
			{
				createBtnText = value;
				OnPropertyChanged(nameof(CreateBtnText));
			}
		}

		private bool _isCreateActive;
		public bool IsCreateActive
		{
			get => _isCreateActive;
			set
			{
				_isCreateActive = value;
				CreateBtnText = value ? TEXT_CONFIRM : TEXT_CREATE;
				OnPropertyChanged(nameof(IsCreateActive));
			}
		}

		private bool isDebugVisible;
		public bool IsDebugVisible
		{
			get => isDebugVisible;
			set
			{
				isDebugVisible = value;
				OnPropertyChanged(nameof(IsDebugVisible));
			}
		}

		public string NewLocationName => entryNewLocationName.Text;
		#endregion

		public MapPage()
		{
			InitializeComponent();
			BindingContext = this;
			//Debug.Log("Init"); //cant debug yet

			//btnCreateLocation.Clicked += LocationManager.CreateLocation;
		}

		public CustomMap GetMap()
		{
			return customMap;
		}


		public void OnStart()
		{
			Debug.Log("OnStart");
			LocationManager.LoadSavedLocations();
		}

		private void BtnToggleDebug_OnClicked(object sender, EventArgs e)
		{
			IsDebugVisible = !IsDebugVisible;
		}

		private void BtnCreateLocation_OnClicked(object sender, EventArgs e)
		{
			IsCreateActive = !IsCreateActive;

			//confirm creation
			if(!IsCreateActive)
			{
				LocationManager.CreateLocation(NewLocationName, 10); //todo: define radius
			}
		}

		private void BtnArrowUp_Clicked(object sender, EventArgs e)
		{
			Debug.Log("move up");
		}
	}
}