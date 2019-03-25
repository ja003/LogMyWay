using Xamarin.Forms.Maps;

namespace LogMyWay
{
	public class CustomMap : Map
	{
		public ICustomMapRenderer Renderer;

		public EGridStep CurrentGridStep = EGridStep.Small;
	}
}
