using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Extensions;
using RUTimetable.Views;
using Xamarin.Forms.Xaml;

namespace RUTimetable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTimetable : ContentView
	{
		ActivityIndicator loading;
        SemesterChangeHandler ChangeHandler;
		bool IsLoading = false;
		public AddTimetable()
		{
			InitializeComponent();
			BindingContext = new AddTimetableViewModel();
            ChangeHandler = new SemesterChangeHandler();
        }
        private async void OpenCampusMap()
        {
            await Navigation.PushPopupAsync(new GetDirectionsPopUp());
        }
        public int CheckPlatform()
		{
			return Device.RuntimePlatform == Device.iOS ? 1: 1;
		}
		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			
		}

		void Butcancel_Clicked(object sender, EventArgs e)
		{
			return;
		}
		void HandleAction()
		{

		}
		void Semester1()
		{
            ChangeHandler.CommunicateWithOtherTabs(1);

        }
        void Semester2()
		{
            ChangeHandler.CommunicateWithOtherTabs(2);
        }
        public async void Settings()
		{
            await Navigation.PushPopupAsync(new Settings());
        }

	}
}
