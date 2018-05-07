using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace RUTimetable.ViewModels
{
    class CampusMapPopUpViewModel
    {
        MapsNewViewModel vm;
        StackLayout layout;
        public CampusMapPopUpViewModel()
        {
            SetUp();
        }
        private void SetUp()
        {
            vm = new MapsNewViewModel();
             layout = new StackLayout
			 {
				VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest=Application.Current.MainPage.Height,
				WidthRequest=Application.Current.MainPage.Width
            };
            layout.Children.Add(vm.GetCustomMap());
          }

        public StackLayout GetStackLayoutWithChildren()
        {
            return layout;
        }
    }
}
