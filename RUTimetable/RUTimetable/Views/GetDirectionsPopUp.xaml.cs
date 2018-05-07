using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Syncfusion.SfAutoComplete;
using Rg.Plugins.Popup.Extensions;
using Acr.UserDialogs;

namespace RUTimetable
{
	public partial class GetDirectionsPopUp : PopupPage
	{
		MapsNewViewModel model;
		RealmDataBase db;
		TKCustomMapViewModel vm;
		public GetDirectionsPopUp()
		{
			InitializeComponent();
			db = new RealmDataBase();
			autoComplete.DataSource = db.GetVenues();
			autoComplete.SuggestionMode = Syncfusion.SfAutoComplete.XForms.SuggestionMode.Contains;
			model = new MapsNewViewModel();
			vm = new TKCustomMapViewModel();
            BindingContext = new TKCustomMapBindingsViewModel();
			main.Children.Add(model.GetCustomMap());
		}
		void GetDirections(object sender,EventArgs e)
		{
			if (string.IsNullOrEmpty(autoComplete.Text)) { UserDialogs.Instance.Alert("Please select a valid location", "Error", "OK"); return; }
			var venue = db.GetVenueByName(autoComplete.Text);
			model.MoveToLocation(venue);
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
