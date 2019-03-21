using LogMyWay.Location;

namespace LogMyWay
{
	public interface ICustomMapRenderer
	{
		void DrawLocation(LocationLog pLocation, EGridStep pStep);
	}
}