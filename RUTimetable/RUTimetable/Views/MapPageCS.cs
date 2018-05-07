using System;
using RUTimetable;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Refractored.FabControl;
using System.Threading.Tasks;
using RUTimetable.ViewModels;

namespace RUTimetable
{
    public class MapPageCS : ContentPage
	{
		SemesterChangeHandler ChangeHandler;
        MapsNewViewModel model;
        FloatingActionButtonViewModel viewModel;
        public MapPageCS ()
        {

	        model = new MapsNewViewModel();
			ChangeHandler = new SemesterChangeHandler();
			viewModel = new FloatingActionButtonViewModel();
            viewModel.AddMapToAbsouluteLayOut(model.GetCustomMap());
            Content =viewModel.GetLayOut();
	        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
	        {
		        Device.BeginInvokeOnMainThread(() => RefreshUI());
		        return true;
	        });

			ToolbarItems.Add(new ToolbarItem("Semester 1", null, new Action(() => Semester1()), ToolbarItemOrder.Secondary, 0));
			ToolbarItems.Add(new ToolbarItem("Semester 2", null, new Action(() => Semester2()), ToolbarItemOrder.Secondary, 0));
			ToolbarItems.Add(new ToolbarItem("Settings", null, new Action(() => Settings()), ToolbarItemOrder.Secondary, 0));

	        }
	        private async void RefreshUI()
	        {

	        Device.BeginInvokeOnMainThread(() =>
	        {
		        model.IsVisible();
		        BindingContext = model;

	        });
	
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
			void Settings()
			{
	
				
		    }
		    protected override void OnAppearing()
		    {
			    Title = "Campus Map";	
		    }
	        }
}
