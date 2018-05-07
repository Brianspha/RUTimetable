using System;
namespace Timetable.Exception
{
	public class TimeTableException:System.Exception
	{
		public TimeTableException(string message):base(message)
		{
		}
	}
}
