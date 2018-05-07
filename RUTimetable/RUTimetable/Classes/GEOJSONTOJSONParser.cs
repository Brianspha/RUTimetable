using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RUTimetable
{
	public class GEOJSONTOJSONParser
	{
		GeoJSONData data;
		RealmDataBase db;
        List<VenueLocation> LocationsWithAssociatedHostedSubjects;
		public GEOJSONTOJSONParser(GeoJSONData dat,List<VenueLocation> ven)
		{
			data = dat;
			db = new RealmDataBase();
			LocationsWithAssociatedHostedSubjects = ven;
		}
		/// <summary>
		/// Process this GeoJSON data into JSON.
		/// Disclaimer Realm does not support storing of Point data structures
		/// 
		/// </summary>
		public void Process()
		{
			var list = data.Data.Split('\n');
			var VList = new List<VenueLocation>();
			var readCord = false;
			var tempLoc = new VenueLocation();
			for (int i = 0; i < list.Length; i++)
			{
				if (list[i].Contains("properties"))
				{
					i++;
					tempLoc.Name = new CommaRemover(list[i++]).RemoveComma();
					tempLoc.Description = list[i].Trim();
					continue;
				}
				if (list[i].Contains("coordinates"))
				{
					var removecommaLat =  new CommaRemover(list[++i]).RemoveComma();				
					var removecommaLong  = new CommaRemover(list[++i]).RemoveComma();
					tempLoc.Lat = double.Parse(removecommaLat, CultureInfo.InvariantCulture);
					tempLoc.Long = double.Parse(removecommaLong, CultureInfo.InvariantCulture);
					readCord = true;
                    var temp = tempLoc.Lat;
                    tempLoc.Lat = tempLoc.Long;
                    tempLoc.Long = temp;
					continue;
				}
				if (readCord)
				{
					VList.Add(tempLoc);
					readCord = false;
					tempLoc = new VenueLocation();
				}
			}
			for(int i=0; i < LocationsWithAssociatedHostedSubjects.Count; i++)
            {
                var casted = LocationsWithAssociatedHostedSubjects[i];
                for(int a=0; a < VList.Count; a++)
                {
					var withoutName = new NameRemover(VList[a].Name).Process();
					bool foundSomething = false;
					var test = casted.Name.Split(new char[] { ' ' });
					var test2 = withoutName.Split(new char[] { ' ' });
					for (int b = 0; b < test.Length; b++)
					{
						for (int c = 0; c < test2.Length; c++)
						{
							var test3 = test[b];
							foundSomething = true;
							if (test[b].Equals(test2[c]))
							{
							for(int d = 0; d<casted.VenueSubjects.Count; d++)
                            {
                            VList[a].VenueSubjects.Add(casted.VenueSubjects[d]);
                            }
						   break;
							}
						}
						if (foundSomething) break;
					}

                }
            }

			db.StoreVenueLocations(VList);
		}


	}
}
