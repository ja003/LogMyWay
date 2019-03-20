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

		public new static App Current;

		public App()
		{
			InitializeComponent();
			Current = this;

			MainPage = new MapPage();

			//MapPage.OnStart();
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
