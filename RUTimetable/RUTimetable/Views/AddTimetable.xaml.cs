using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Extensions;
using RUTimetable.Views;

namespace RUTimetable
{
	public partial class AddTimetable : ContentPage
	{
		ActivityIndicator loading;
        SemesterChangeHandler ChangeHandler;
		bool IsLoading = false;
		public AddTimetable()
		{
			InitializeComponent();
			BindingContext = new AddTimetableViewModel();
            ChangeHandler = new SemesterChangeHandler();
            ToolbarItems.Add(new ToolbarItem("Semester 1", null, new Action(() => Semester1()), ToolbarItemOrder.Secondary, CheckPlatform()));
            ToolbarItems.Add(new ToolbarItem("Semester 2", null, new Action(() => Semester2()), ToolbarItemOrder.Secondary, CheckPlatform()));
            ToolbarItems.Add(new ToolbarItem("Settings", null, new Action(() => Settings()), ToolbarItemOrder.Secondary, CheckPlatform()));
            ToolbarItems.Add(new ToolbarItem("Campus Map", null, new Action(() => OpenCampusMap()), ToolbarItemOrder.Secondary));
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
        protected override void OnAppearing()
		{
			Title = "Add Timetable";
		}

	}
}
