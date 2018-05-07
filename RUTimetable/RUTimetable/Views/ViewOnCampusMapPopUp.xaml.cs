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
    public partial class ViewOnCampusMapPopUp : PopupPage
    {
        CampusMapPopUpViewModel vm;
        public ViewOnCampusMapPopUp()
        {
            InitializeComponent();
            vm = new CampusMapPopUpViewModel();
			var but = new Button { BackgroundColor = Color.White, Text = "Close" ,VerticalOptions=LayoutOptions.End};
            but.Clicked += OnClose;
            vm.GetStackLayoutWithChildren().Children.Add(but);
            Content =vm.GetStackLayoutWithChildren();
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
    }
}