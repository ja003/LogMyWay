using Android.Content;
using Android.Gms.Maps;
using LogMyWay;
using LogMyWay.Droid;
using LogMyWay.Location;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace LogMyWay.Droid
{
	 /// <summary>
	 /// Visual effects added to the map
	 /// </summary>
	 public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
	 {

		  private CustomMap customMap;

		  public CustomMapRenderer(Context context) : base(context)
		  {
				//MessagingCenter.Subscribe<CustomMap>(this, "LogPosition", OnLogPosition);
				//MessagingCenter.Subscribe<CustomMap, Position>(this, "LogPosition", (source, arg) => { OnLogPosition(arg); });

		  }


		  ///OVERRIDES
		  private bool onMapReadyCalled;

		  protected override void OnMapReady(GoogleMap googleMap)
		  {
				base.OnMapReady(googleMap);

				if(onMapReadyCalled)
					 return;
				onMapReadyCalled = true;

				//move to current location only at first call
				LocationManager.MoveToCurrentPosition();
		  }

		  /// <summary>
		  /// We override the OnElementChanged() event handler to get the desired instance. We also use it for updates.
		  /// </summary>
		  /// <param name="e">It contains either the NewElement or the OldElement</param>
		  protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
		  {
				base.OnElementChanged(e);

				//todo: find out when it is called...does it have to be called always?
				if(e.NewElement != null)
				{
					 customMap = e.NewElement as CustomMap;
					 Control.GetMapAsync(this);
				}
		  }
	 }
}