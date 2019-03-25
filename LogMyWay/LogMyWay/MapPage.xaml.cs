using LogMyWay.Location;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
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


		private Position currentDebugPosition;

		public MapPage()
		{
			InitializeComponent();
			BindingContext = this;
			//Debug.Log("Init"); //cant debug yet

			//btnCreateLocation.Clicked += LocationManager.CreateLocation;
		}
			  
		public void OnStart()
		{
			Debug.Log("OnStart");
			LocationManager.LoadSavedLocations();
		}

		public CustomMap GetMap()
		{
			return customMap;
		}

		/// <summary>
		/// Load location names into picker
		/// </summary>
		internal void OnLocationsLoaded()
		{
			foreach(LocationLog location in LocationManager.Locations)
			{
				pickerLocation.Items.Add(location.Name);
			}
		}
		
		/// <summary>
		/// Select active location
		/// </summary>
		internal void OnLocationSet()
		{
			Debug.Log($"OnLocationSet {LocationManager.CurrentLocation.Name}");
			currentDebugPosition = LocationManager.CurrentLocation.Center;

			pickerLocation.SelectedIndex = LocationManager.Locations.IndexOf(LocationManager.CurrentLocation);
		}
			  
		private void PickerLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
			string locationName = pickerLocation.Items[pickerLocation.SelectedIndex];
			LocationManager.SetActiveLocation(locationName);
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


		#region arrows
		private void BtnArrowUp_Clicked(object sender, EventArgs e)
		{
			MoveDebugPosition(EDirection.Up);
		}

		private void BtnArrowLeft_Clicked(object sender, EventArgs e)
		{
			MoveDebugPosition(EDirection.Left);
		}

		private void BtnArrowDown_Clicked(object sender, EventArgs e)
		{
			MoveDebugPosition(EDirection.Down);
		}

		private void BtnArrowRight_Clicked(object sender, EventArgs e)
		{
			MoveDebugPosition(EDirection.Right);
		}

		private void MoveDebugPosition(EDirection pDirection)
		{
			Position move = new Position(0, 0);
			switch(pDirection)
			{
				case EDirection.Up:
					move = new Position(GridValues.MIN_GRID_STEP, 0);
					break;
				case EDirection.Right:
					move = new Position(0, GridValues.MIN_GRID_STEP);
					break;
				case EDirection.Down:
					move = new Position(-GridValues.MIN_GRID_STEP, 0);
					break;
				case EDirection.Left:
					move = new Position(0, -GridValues.MIN_GRID_STEP);
					break;

			}
			currentDebugPosition = new Position(
				currentDebugPosition.Latitude + move.Latitude,
				currentDebugPosition.Longitude + move.Longitude
				);

			LocationManager.LogPosition(currentDebugPosition);
		}
		#endregion

		private void BtnLog_Clicked(object sender, EventArgs e)
		{
			Debug.Clear();
			//LocationManager.LogPosition(currentDebugPosition);
		}

		#region grid step

		private void BtnGridStepSmall_Clicked(object sender, EventArgs e)
		{
			SetGridStep(EGridStep.Small);
		}

		private void BtnGridStepMedium_Clicked(object sender, EventArgs e)
		{
			SetGridStep(EGridStep.Medium);
		}

		private void BtnGridStepLarge_Clicked(object sender, EventArgs e)
		{
			SetGridStep(EGridStep.Large);
		}

		private void SetGridStep(EGridStep pStep)
		{
			GetMap().SetGridStep(pStep, true);
		}
		#endregion

		private enum EDirection
		{
			Up,
			Right,
			Down,
			Left
		}

		private void BtnClearMap_Clicked(object sender, EventArgs e)
		{
			LocationManager.CurrentLocation.Clear();
			customMap.Renderer.DrawLocation(LocationManager.CurrentLocation);
		}
	}
}