using System;
namespace RUTimetable
{
	public class SpaceRemover
	{
		public string RemovedSpaces { get; set; }
		public SpaceRemover(string item)
		{
			RemoveSpace(item);
		}
		public void RemoveSpace(string item)
		{
			var newString = "";
			var temp = item.ToCharArray();
			for (int i = 0; i < temp.Length; i++) {
				if (temp[i] != ' ') newString += temp[i];
			}
			RemovedSpaces = newString;
		}
	}
}
