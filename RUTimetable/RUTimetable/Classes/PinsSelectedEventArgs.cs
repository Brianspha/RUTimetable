using System;
using TK.CustomMap;

namespace RUTimetable
{
	public class PinSelectedEventArgs : EventArgs
	{
		public TKCustomMapPin Pin { get; private set; }

		public PinSelectedEventArgs(TKCustomMapPin pin)
		{
			this.Pin = pin;
		}
	}
}
