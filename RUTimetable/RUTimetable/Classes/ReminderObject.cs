using System;
using Xamarin.Forms;

namespace RUTimetable
{
	public class ReminderObject
	{
		//Used for Holding information regarding 
		//The current subject state this will be used
		//for command interface 
		public Switch zwitch { get; set; }
		public Label Name { get; set; }
		public Label Time { get; set; }
		public Label Period { get; set; }
	}
}
