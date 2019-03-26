using LogMyWay.Location;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LogMyWay
{
	public partial class App : Application
	{
		public static double ScreenHeight;
		public static double ScreenWidth;

		public CustomMap Map => MapPage.GetMap();
		public MapPage MapPage => (MapPage) MainPage;

		private const string lastActiveLocationKey = "LastActiveLocation";
		public string LastActiveLocation
		{
			get
			{
				if(Current.Properties.ContainsKey(lastActiveLocationKey))
					return (string)Current.Properties[lastActiveLocationKey];
				return null;
			}
			set
			{
				Current.Properties[lastActiveLocationKey] = value;
			}
		}

		private const string lastGridStepKey = "LastGridStep";
		public EGridStep LastGridStep
		{
			get
			{
				if(Current.Properties.ContainsKey(lastGridStepKey))
					return (EGridStep)Current.Properties[lastGridStepKey];
				return EGridStep.Small;
			}
			set
			{
				Current.Properties[lastGridStepKey] = (int)value;
			}
		}

		public new static App Current;

		//IGeolocator locator;

		public App()
		{
			InitializeComponent();
			Current = this;

			MainPage = new MapPage();


			//MapPage.OnStart();
			//locator = CrossGeolocator.Current;

			//locator.StartListeningAsync(new TimeSpan(0, 0, 5), 1); //min 5 secs between updates, min 1 meter difference

			//locator.PositionChanged += GpsManager.OnPositionChanged;

			/*locator.PositionChanged += (sender, e) => {
				var position = e.Position;

				latitudeLabel.Text = position.Latitude;
				longitudeLabel.Text = position.Longitude;
			};*/
		}

		//public event EventHandler OnStarted;

		protected override void OnStart()
		{
			// Handle when your app starts
			//Current = this;
			//MapPage.OnStart();
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
