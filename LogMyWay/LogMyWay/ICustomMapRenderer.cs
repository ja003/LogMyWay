using LogMyWay.Location;
using Xamarin.Forms.Maps;

namespace LogMyWay
{
	public interface ICustomMapRenderer
	{
		void DrawLocation(LocationLog pLocation);
		void DrawLoggedPosition(Position pPosition);
		void OnSetGridStep(bool pRedraw);
		void DrawDebugPosition(Position pPosition);
	}
}