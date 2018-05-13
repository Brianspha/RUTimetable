using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RUTimetable.Classes
{
    public class SlotIndexFontConversion : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object temp = "";
            if (Device.RuntimePlatform.ToString() == Device.Android)
            {
                if (parameter != null && parameter is string)
                    temp= "RUTimetable.ttf#" + parameter.ToString();
                else
                    temp= "RUTimetable.ttf";
            }
            else if (Device.RuntimePlatform.ToString() == Device.iOS)
            {

                temp= "RUTimetable";

            }
            return temp;
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}
