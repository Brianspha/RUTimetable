using System;
namespace RUTimetable
{
	public class DayConverter
	{
		int index;
		public DayConverter(int index)
		{
			this.index = index;
		}
		public DayConverter()
		{
		}
		public int ConvertDayToInt(string day)
		{
			int converted = -1;
			switch (day)
			{
				case "Monday":
					converted= 0;
					break;
				case "Tuesday":
					converted = 1;
					break;
				case "Wednesday":
					converted = 2;
					break;
				case "Thursday":
					converted = 3;
					break;
				case "Friday":
					converted = 4;
					break;	
			}
			return converted;
		}
		public string ConvertDayToString(int day)
		{
			var converted = "";
			switch (day)
			{
				case 0:
					converted = "Monday";
					break;
				case 1:
					converted = "Tuesday";
					break;
				case 2:
					converted = "Wednesday";
					break;
				case 3:
					converted = "Thursday";
					break;
				case 4:
					converted = "Friday";
					break;
			}
			return converted;
		}
		/// <summary>
		/// Converts to day.
		/// </summary>
		/// <returns>The the String representation of the day of week. The reason there are 10 indexes the way in which the data thats returned by the website forces one to have 10 ndexes forst 1-5 represents semester one second represents semester 2.</returns>
		public string ConvertToDay()
		{
			switch (index)
			{
				case 1:
					return "Monday\r";
				case 2:
					return "Tuesday\r";
				case 3:
					return "Wednesday\r";
				case 4:
					return "Thursday\r";
				case 5:
					return "Friday\r";
				case 6:
					return "Monday\r";
				case 7:
					return "Tuesday\r";
				case 8:
					return "Wednesday\r";
				case 9:
					return "Thursday\r";
				case 10:
					return "Friday\r";
			}
			return "";
		}
		/// <summary>
		/// Converts to day of week.
		/// </summary>
		/// <returns>The to day of week.</returns>
		/// <param name="dayofWeek">Dayof week.</param>
		public DayOfWeek ConvertToDayOfWeek(string dayofWeek)
		{
			var day = DayOfWeek.Monday;//defualt
			switch (dayofWeek)
			{
				case "Monday":
					day = DayOfWeek.Monday;
					break;
				case "Tuesday":
					day = DayOfWeek.Tuesday;
					break;
				case "Wednesday":
					day = DayOfWeek.Wednesday;
					break;
				case "Thursday":
					day = DayOfWeek.Thursday;
					break;
				case "Friday":
					day = DayOfWeek.Friday;
					break;
			}
			return day;
		}
	}
}
