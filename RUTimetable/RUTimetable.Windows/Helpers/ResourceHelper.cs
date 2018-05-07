using System.IO;
using Windows.Storage;

namespace RUTimetable.Windows.Helpers
{
    public  class ResourceHelper
    {
        public ResourceHelper()
        {

        }
        public async System.Threading.Tasks.Task<GeoJSONData> Process()
        {
            var content = "";
            using (StreamReader text = new StreamReader(await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync("ms-appx:///Assets/RhodesMap.geojson")))
            {
                var temp =  text.ReadLine();
                while (true)
                {
                    if (string.IsNullOrEmpty(temp)) break;
                    content += temp + "\n";
                    temp = text.ReadLine();
                }

            }
            return new GeoJSONData { Data = content };
        }
    }
}
