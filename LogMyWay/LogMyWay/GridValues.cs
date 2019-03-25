using LogMyWay.Location;
using Xamarin.Forms.Maps;

namespace LogMyWay
{
	/// <summary>
	/// Calculates values related to grid generation
	/// </summary>
	public static class GridValues
	{
		public const double MIN_GRID_STEP = 0.001f;

		private static int GetRadiusStep(EGridStep pStep)
		{
			int radiusStep = 1;
			switch(pStep)
			{
				case EGridStep.Small:
					radiusStep = 1;
					break;
				case EGridStep.Medium:
					radiusStep = 2;
					break;
				case EGridStep.Large:
					radiusStep = 3;
					break;
			}
			return radiusStep;
		}

		public static double GetStepSize(EGridStep pStep)
		{
			return MIN_GRID_STEP * GetRadiusStep(pStep);
		}

		/// <summary>
		/// Calculate corner of given location
		/// pTopLeft = false => botRight
		/// </summary>
		public static Position GetLocationCorner(LocationLog pLocation, bool pTopLeft)
		{
			int multiplier = pTopLeft ? 1 : -1;
			Position corner = new Position(
					pLocation.Center.Latitude + multiplier * pLocation.RadiusSteps * MIN_GRID_STEP,
					pLocation.Center.Longitude - multiplier * pLocation.RadiusSteps * MIN_GRID_STEP);
			return corner;
		}

		/// <summary>
		/// Conversion of radius steps based on GridStep. 
		/// EGridStep.Small => returns pLocationRadiusSteps
		/// </summary>
		public static int GetStepsCount(int pLocationRadiusSteps, EGridStep pStep)
		{
			return pLocationRadiusSteps / GetRadiusStep(pStep);
		}
	}
}
