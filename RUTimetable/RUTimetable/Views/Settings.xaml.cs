using Acr.UserDialogs;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using RUTimetable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RUTimetable.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : PopupPage
    {
        int secs=0;
        RealmDataBase db;
        public Settings()
        {
            InitializeComponent();
            db = new RealmDataBase();
            seconds.ItemsSource = MiscellaneousProvider.MinuteProvider;
            seconds.SelectedItem = (new RealmDataBase().GetMinutesToSendBeforeTime()/60);
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
        private async void OnClose(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
        void Handle_Click_Seconds(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                if (picker.ItemsSource[selectedIndex].ToString() != "Select no of minutes to be reminded before lecture starts")
                {
                    secs = int.Parse(picker.ItemsSource[selectedIndex].ToString());
                }
                else
                {
                    UserDialogs.Instance.Alert("Please select a valid number");
                }
            }

        }
        public async void Save(object sender, System.EventArgs e)
        {
            db.SetMinutesToSendBeforeTime(secs);
            await Navigation.PopPopupAsync();

        }
    }
}