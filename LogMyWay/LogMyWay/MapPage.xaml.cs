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
		  }

		  public CustomMap GetMap()
		  {
				return customMap;
		  }
	 }
}