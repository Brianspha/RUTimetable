using Rg.Plugins.Popup.Extensions;
using RUTimetable.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RUTimetable
{
	public partial class AddSubjectToVenue : ContentPage
	{
		SemesterChangeHandler ChangeHandler;
		public AddSubjectToVenue()
		{
			InitializeComponent();
            ToolbarItems.Add(new ToolbarItem("Semester 1", null, new Action(() => Semester1()), ToolbarItemOrder.Secondary, CheckPlatform()));
            ToolbarItems.Add(new ToolbarItem("Semester 2", null, new Action(() => Semester2()), ToolbarItemOrder.Secondary, CheckPlatform()));
            ToolbarItems.Add(new ToolbarItem("Settings", null, new Action(() => SettingsAsync()), ToolbarItemOrder.Secondary, CheckPlatform()));
        }
        public int CheckPlatform()
		{
			return Device.RuntimePlatform == Device.iOS ? 0 : 1;
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
    }
}
