using LogMyWay.Location;
using Xamarin.Forms.Maps;

namespace LogMyWay
{
	public interface ICustomMapRenderer
	{
		void DrawLocation(LocationLog pLocation);
		void DrawLoggedPosition(Position pPosition);
	}
}