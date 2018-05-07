using System;
namespace RUTimetable
{
	public class MonthProvider
	{
		public MonthProvider()
		{
		}
		public string GetMonth(string index)
		{
			string temp = "";
			switch (index)
			{
				case "1":
					temp = "January";
					break;
				case "2":
					temp = "February";
					break;
				case "3":
					temp = "March";
					break;
				case "4":
					temp = "April";
					break;
				case "5":
					temp = "May";
					break;
				case "6":
					temp = "June";
					break;
				case "7":
					temp = "July";
					break;
				case "8":
					temp = "August";
					break;
				case "9":
					temp = "September";
					break;
				case "10":
					temp = "October";
					break;
				case "11":
					temp = "November";
					break;
				case "12":
					temp = "December";
					break;

			}
			return temp;
		}
	}
}
