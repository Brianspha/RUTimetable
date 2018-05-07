using System;
using RUTimetable.ViewModels;
using Xamarin.Forms;
using Rg.Plugins;
using Rg.Plugins.Popup.Extensions;
using RUTimetable.Views;

namespace RUTimetable
{
    public partial class Wednesday : ContentPage
    {
        DayViewModel model;
        FloatingActionButtonViewModel viewModel;
        SemesterChangeHandler ChangeHandler;
		bool added = false;
        /// <summary>
        /// Initializes a new instance of the <see cref="T:RUTimetable.Monday"/> class.
        /// </summary>
        public Wednesday()
        {
            InitializeComponent();
            viewModel = new FloatingActionButtonViewModel(Navigation);
            ChangeHandler = new SemesterChangeHandler();
            model = new DayViewModel("Wednesday", DateTime.UtcNow.Month > 6 ? 2 : 1);
            if (model.Count() == 0)
            {
                var contentPage = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Orientation = StackOrientation.Vertical,
                    Children = {
                        new Label{
                            Text="No Time table Yet or You are a Postgrad student",
                            VerticalTextAlignment=TextAlignment.Center,
                            HorizontalTextAlignment=TextAlignment.Center,
                            VerticalOptions=LayoutOptions.FillAndExpand,
                            HorizontalOptions=LayoutOptions.CenterAndExpand
                        }
                    }
                };
                viewModel.AddContentPageToAbsoluteLayOut(contentPage);
                Content = viewModel.GetLayOut();
            }
            else
            {
                viewModel.AddDayLayOutToAbsoluteLayOut(listView);
                Content = viewModel.GetLayOut();
                BindingContext = model;
            }
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                Device.BeginInvokeOnMainThread(() => RefreshUI());
                return true;
            });
            ToolbarItems.Add(new ToolbarItem("Semester 1", null, new Action(() => Semester1()), ToolbarItemOrder.Secondary));
            ToolbarItems.Add(new ToolbarItem("Semester 2", null, new Action(() => Semester2()), ToolbarItemOrder.Secondary));
            ToolbarItems.Add(new ToolbarItem("Settings", null, new Action(() => SettingsAsync()), ToolbarItemOrder.Secondary));
        }
        public int CheckPlatform()
		{
			return Device.RuntimePlatform == Device.iOS ? 1 : 1;
		}
        void SwitchToggled(object sender, ToggledEventArgs e)
        {

        }
		private async void RefreshUI()
		{

			Device.BeginInvokeOnMainThread(() =>
			{

				var anythingToload = ChangeHandler.GetSemester();
				if (anythingToload.semester > 0)
				{
					model = new DayViewModel("Wednesday", anythingToload.semester);
					viewModel.AddDayLayOutToAbsoluteLayOut(listView);
					Content = viewModel.GetLayOut();
					BindingContext = model;

				}
				else
				{
					model.Populate();
					if (model.Count() == 0) return;
					viewModel.AddDayLayOutToAbsoluteLayOut(listView);
					Content = viewModel.GetLayOut();
					BindingContext = model;
				}
			});
		}        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var s = listView.SelectedItem;
            var test = s as CardViewTemplate;
            //test.BackgroundColor = Color.White;
        }
        /// <summary>
        /// Semester1 the specified sender and e.
        /// </summary>
        /// <returns>The semester1.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void Semester1()
        {
            ChangeHandler.CommunicateWithOtherTabs(1);
        }
        /// <summary>
        /// Semester2 the specified sender and e.
        /// </summary>
        /// <returns>The semester2.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void Semester2()
        {
            ChangeHandler.CommunicateWithOtherTabs(2);
        }
        /// <summary>
        /// Settings the specified sender and e.
        /// </summary>
        /// <returns>The settings.</returns>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
		public async void SettingsAsync()
        {
            await Navigation.PushPopupAsync(new Settings());
        }
        /// <summary>
        /// Ons the appearing.
        /// </summary>
        protected override void OnAppearing()
        {
            Title = "Wednesday";
            Icon = "W.png";
            RefreshUI();
        }
    }
}
