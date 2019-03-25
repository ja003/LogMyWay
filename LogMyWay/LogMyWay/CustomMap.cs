using Xamarin.Forms.Maps;

namespace LogMyWay
{
	public class CustomMap : Map
	{
		public ICustomMapRenderer Renderer;

		//public EGridStep CurrentGridStep { get; private set; } = EGridStep.Small;

		public void SetGridStep(EGridStep pStep, bool pRedraw)
		{
			Debug.Log($"SetGridStep {pStep} - {pRedraw}");
			App.Current.LastGridStep = pStep;
			//CurrentGridStep = pStep;
			Renderer.OnSetGridStep(pRedraw);
		}
	}
}
