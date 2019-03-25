using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace LogMyWay.Structures
{
	public struct GridArea
	{
		public Position TopLeft { get; private set; }
		public Position BotRight { get; private set; }

		public GridArea(Position pTopLeft, Position pBotRight) : this()
		{
			TopLeft = pTopLeft;
			BotRight = pBotRight;
		}
	}
}
