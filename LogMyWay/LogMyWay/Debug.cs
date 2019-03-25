using System;

namespace LogMyWay
{
	public static class Debug
	{
		public static void Log(string pText)
		{
			App.Current.MapPage.DebugText += Environment.NewLine + pText;
		}

		public static void Clear()
		{
			App.Current.MapPage.DebugText = "cleared";
		}
	}
}