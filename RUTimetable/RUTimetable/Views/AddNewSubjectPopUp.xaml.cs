using Acr.UserDialogs;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;
using RUTimetable.ViewModels;

namespace RUTimetable
{
	public partial class AddNewSubjectPopUp : PopupPage
	{
		int semester = 0, period = 0;
		string day = "";
        AddNewSubjectViewModel view;
        public AddNewSubjectPopUp()
        {

            InitializeComponent();
			BindingContext = new AddNewSubjectViewModel(this.Navigation);
            view = new AddNewSubjectViewModel(Navigation);
			Semester.ItemsSource = MiscellaneousProvider.SemesterProvider;
			Period.ItemsSource = MiscellaneousProvider.PeriodProvider;
			Day.ItemsSource = MiscellaneousProvider.Days;
        }
        void SetUp()
        {
        }
		protected override void OnAppearing()
		{
			base.OnAppearing();
		}
		private void OnCloseButtonTapped(object sender, EventArgs e)
		{
			CloseAllPopup();
		}
		protected override bool OnBackgroundClicked()
		{
			CloseAllPopup();

			return false;
		}
		private async void CloseAllPopup()
		{
			await Navigation.PopAllPopupAsync();
		}
		void Handle_Click_Period(object sender, EventArgs e)
		{
			var picker = (Picker)sender;
			int selectedIndex = picker.SelectedIndex;
			if (selectedIndex != -1)
			{
				period = Convert.ToInt32(picker.ItemsSource[selectedIndex]);
			}

		}
		void Handle_Click_Semester(object sender, EventArgs e)
		{
			var picker = (Picker)sender;
			int selectedIndex = picker.SelectedIndex;
			if (selectedIndex != -1)
			{
				semester = Convert.ToInt32(picker.ItemsSource[selectedIndex]);
			}

		}
		void Handle_Click_Day(object sender, EventArgs e)
		{
			var picker = (Picker)sender;
			int selectedIndex = picker.SelectedIndex;
			if (selectedIndex != -1)
			{
				day = picker.ItemsSource[selectedIndex].ToString();
			}

		}
		void HandleAction()
		{
		}
		public async Task Loading()
		{
			Random rnd = new Random();
			int wait1 = rnd.Next(5000, 10000);
			await Task.Delay(wait1);
			int wait2 = rnd.Next(5000, 10000);
			await Task.Delay(wait2);
		}
        async void Save_Clicked(object sender, System.EventArgs e)
        {
            RealmDataBase db = new RealmDataBase();
            var student = db.GetStudent();
            if (student == null){ UserDialogs.Instance.Alert("Please add your timetable using your student number in the Add timetable screen before proceeding with \n adding any addittional subjects, the application only works for undergraduate \n students whos timetable is stored on the universities timetable database."); return; }
            if (student != null && !string.IsNullOrEmpty(Subject.Text) && !string.IsNullOrEmpty(day) && period > 0 && semester > 0)
			{
				UserDialogs.Instance.ShowLoading("Saving Please Wait", MaskType.Black);
				await Loading();
				db.Write(Subject.Text, period.ToString(), new TimeProvider().GetPeriodTime(period.ToString()), semester, day);
				UserDialogs.Instance.HideLoading();
			}
			else
			{
		    	await DisplayAlert("Error!", "Please fill all the required information!!", "OK");
			}
		}
		private async void OnClose(object sender, EventArgs e)
		{
			await Navigation.PopPopupAsync();
		}
	}
}
