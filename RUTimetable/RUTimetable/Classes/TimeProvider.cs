using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace RUTimetable
{
	public class TimeProvider
	{
		public TimeProvider()
		{
		}
		public List<string> TimeForAlarm()
		{
			var tempList = new List<string>();
			tempList.Add("07:45:00");
			tempList.Add("08:40:00");
			tempList.Add("09:35:00");
			tempList.Add("10:30:00");
			tempList.Add("11:25:00");
			tempList.Add("12:20:00");
			tempList.Add("14:15:00");
			tempList.Add("15:10:00");
			tempList.Add("16:05:00");
			tempList.Add("17:00:00");
			return tempList;
		}
		public string ConvertLectureTimeToAlarmTime(string time)
		{
			var temp = "";
			switch (time)
			{
				case "7:45 am":
				temp = "07:45:00";
				break;
				case "8:40 am":
			    temp = "08:40:00";
				break;
 			  	case "9:35 am":
				temp = "09:35:00";
				break;
				case "10:30 am":
				temp = "10:30:00";
		    	break;
	         	case "11:25 am":
			   temp = "11:25:00";
			   break;
				case "12:20 pm":
				temp = "12:20:00";
				break;
				case "14:15 pm":
			   temp = "14:15:00";
		     	break;
				case "15:10 pm":
				temp = "15:10:00";
				break;
				case "16:05 pm":
				temp = "16:05:00";
				break;
				case "17:00 pm":
				temp = "17:00:00";
				break;
			}
			return temp;
		}
		public string GetPeriodTime(string time)
		{
			var temp = "";
			switch (time)
			{
				case "1":
					temp = "7:45 am";
					break;
				case "2":
					temp = "8:40 am";
					break;
				case "3":
					temp = "9:35 am";
					break;
				case "4":
					temp = "10:30 am";
					break;
				case "5":
					temp = "11:25 am";
					break;
				case "6":
					temp = "12:20 pm";
					break;
				case "7":
					temp = "14:15 pm";
					break;
				case "8":
					temp = "15:10 pm";
					break;
				case "9":
					temp = "16:05 pm";
					break;
				case "10":
					temp = "17:00 pm";
					break;
			}
			return temp;
		}
		public string ConvertAlarmTimeToLectureTime(string time)
		{
			var temp = "";
			switch (time)
			{
				case "7:45:00":
					temp = "7:45 am";
					break;
				case "8:40:00":
					temp = "08:40 am";
					break;
				case "9:35:00":
					temp = "09:35 am";
					break;
				case "10:30:00":
					temp = "10:30 am";
					break;
				case "11:25:00":
					temp = "11:25 am";
					break;
				case "12:20:00":
					temp = "12:20 pm";
					break;
				case "14:15:00":
					temp = "14:15 pm";
					break;
				case "15:10:00":
					temp = "15:10 pm";
					break;
				case "16:05:00":
					temp = "16:05 pm";
					break;
				case "17:00:00":
					temp = "17:00 pm";
					break;
			}
			return temp;
			
		}
		public List<string> TimeForLectures()
		{
			var tempList = new List<string>();
			tempList.Add("7:45 am");
			tempList.Add("8:40 am");
			tempList.Add("9:35 am");
			tempList.Add("10:30 am");
			tempList.Add("11:25 am");
			tempList.Add("12:20 pm");
			tempList.Add("14:15 pm");
			tempList.Add("15:10 pm");
			tempList.Add("16:05 pm");
			tempList.Add("17:00 pm");
			return tempList;
		}
	}
}
