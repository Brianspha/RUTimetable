using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace RUTimetable
{
	public partial class AddNewVenue : ContentPage
	{
		SemesterChangeHandler ChangeHandler;
		public AddNewVenue()
		{
			InitializeComponent();
			BindingContext = new AddNewVenueViewModel();
		}
		/// <summary>
		/// Semester1 the specified sender and e.
		/// </summary>
		/// <returns>The semester1.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void Semester1(object sender, System.EventArgs e)
		{
			ChangeHandler.CommunicateWithOtherTabs(1);
		}
		/// <summary>
		/// Semester2 the specified sender and e.
		/// </summary>
		/// <returns>The semester2.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void Semester2(object sender, System.EventArgs e)
		{
			ChangeHandler.CommunicateWithOtherTabs(2);
		}
		/// <summary>
		/// Settings the specified sender and e.
		/// </summary>
		/// <returns>The settings.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void Settings(object sender, System.EventArgs e)
		{	
				
		}
	}
}
