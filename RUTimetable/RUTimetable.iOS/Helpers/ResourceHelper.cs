using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using RUTimetable;
using Xamarin;
namespace RUTimetableIOS.Helpers
{
    class ResourceHelper
    {
		List<VenueLocation> loc;
        public ResourceHelper()
        {
			loc = new List<VenueLocation>();
        }
        public GeoJSONData Process()
        {
            var content = "";
            var content2 = "";
            var text = System.IO.File.OpenText("Files/RhodesMap.geojson");
            string temp = text.ReadLine();
            while (true)
            {
                if (string.IsNullOrEmpty(temp)) break;
                content += temp + "\n";
                temp = text.ReadLine();
            }
            GeoJSONData geoData = new GeoJSONData { Data = content };
            text = System.IO.File.OpenText("Files/Venues.txt");
            temp = text.ReadLine();
            var tempList = new List<VenueLocation>();
            while (true)
            {
                if (string.IsNullOrEmpty(temp)) break;
                var subject = "";
                var index = 0;
                for(int i =0; i < temp.Length; i++,index++)
                {
					if (temp[i] != '[' ) subject += temp[i];
						else
						{
							index++; //skip over the '['
							break;
						}                
				}
                var venue = "";
				subject = new SpaceRemover(subject).RemovedSpaces;
				for (int a = index; a<temp.Length; a++) {
					if (temp[a] == ']') break;
					else venue += temp[index++];
				}          
				var Sub = new Subject { Name = subject };
				bool Added = false;
                for (int i = 0; i<tempList.Count; i++)
                {
					if (tempList[i].Name == venue)
                    {
                        tempList[i].VenueSubjects.Add(Sub);
						Added = true;
                    }

                }
				var tempVenueLoc = new VenueLocation { Name = venue };
				tempVenueLoc.VenueSubjects.Add(Sub);
				if (!Added) tempList.Add(tempVenueLoc);
                temp = text.ReadLine();
            }
			loc = tempList;
            return geoData;

        }
		public List<VenueLocation> GetParsedVenuesWithSubjects()
		{
			return loc;
		}
        
    }
}