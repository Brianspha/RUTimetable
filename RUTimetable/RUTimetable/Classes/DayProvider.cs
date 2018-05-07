using System;
namespace RUTimetable
{
	public class DayProvider
	{
		public DayProvider()
		{
		}
		/// <summary>
		/// Gets the next weekday.
		/// </summary>
		/// <returns>The next weekday.</returns>
		/// <param name="start">Start.</param>
		/// <param name="day">Day.</param>
		public DateTime GetNextWeekday(DateTime start, DayOfWeek day)
		{
			// The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
			int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
			return start.AddDays(daysToAdd);
		}
	}
}
