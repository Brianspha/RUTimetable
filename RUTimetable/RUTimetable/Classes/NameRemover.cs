using System;

namespace RUTimetable
{
	class NameRemover
	{
		string venueLocation;

		public NameRemover(string venueLocation)
		{
			this.venueLocation = venueLocation;
		}

		internal string Process()
		{
			int index = 0;
			var temp = "";
			while (venueLocation[index++] != ':') ;
			index++;
			while (venueLocation.Length > index) { if (venueLocation[index] == '"') { index++; continue; } temp += venueLocation[index++]; }
			return temp;
		}
	}
}