using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;

namespace LogMyWay.Droid
{
	 [Activity(Label = "LogMyWay", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	 public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	 {
		  private Bundle _savedInstanceState;
		  private const int REQUEST_CODE_AccessFineLocation = 1000;

		  protected override void OnCreate(Bundle savedInstanceState)
		  {
				base.OnCreate(savedInstanceState);
				_savedInstanceState = savedInstanceState;

				//request permission
				ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessFineLocation }, REQUEST_CODE_AccessFineLocation);

				//init will be called after permission os granted
		  }

		  private void Init()
		  {
				TabLayoutResource = Resource.Layout.Tabbar;
				ToolbarResource = Resource.Layout.Toolbar;

				global::Xamarin.Forms.Forms.Init(this, _savedInstanceState);
				global::Xamarin.FormsMaps.Init(this, _savedInstanceState);

				SetSize();

				LoadApplication(new App());
		  }

		  /// <summary>
		  /// Store screen size - map is then scaled based on these values
		  /// </summary>
		  private void SetSize()
		  {
				var width = Resources.DisplayMetrics.WidthPixels;
				var height = Resources.DisplayMetrics.HeightPixels;
				var density = Resources.DisplayMetrics.Density;

				App.ScreenWidth = (width - 0.5f) / density;
				App.ScreenHeight = (height - 0.5f) / density;
		  }

		  /// <summary>
		  /// Calls Init after location permission is obtained
		  /// </summary>
		  public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		  {
				switch(requestCode)
				{
					 case REQUEST_CODE_AccessFineLocation:
						  break;
					 default:
						  return;
				}

				base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

				Init();
		  }
	 }
}