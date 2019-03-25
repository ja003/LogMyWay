using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace LogMyWay.Structures
{
	public struct GridLine
	{
		public Position Start { get; private set; }
		public Position End { get; private set; }

		public GridLine(Position pStart, Position pEnd) : this()
		{
			Start = pStart;
			End = pEnd;
		}
	}
}
