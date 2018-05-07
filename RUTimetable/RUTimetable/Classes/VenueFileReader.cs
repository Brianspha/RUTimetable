using System;
using Xamarin.Forms;
using System.Reflection;
using System.IO;

namespace RUTimetable.Classes
{
    public class VenueFileReader
    {
        public VenueFileReader()
        {
            var assembly = typeof(VenueFileReader).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("RhodesMap.geojson");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            var h = 4; 
        }
    }
  }
