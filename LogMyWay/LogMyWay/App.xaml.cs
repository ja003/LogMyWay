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

		  public CustomMap Map => ((MapPage)MainPage).GetMap();

		  public new static App Current;

		  public App()
		  {
				InitializeComponent();

				MainPage = new MapPage();
		  }

		  protected override void OnStart()
		  {
				// Handle when your app starts
				Current = this;
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
