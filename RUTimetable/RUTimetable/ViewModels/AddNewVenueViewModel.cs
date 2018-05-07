using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace RUTimetable
{
	public class AddNewVenueViewModel : INotifyPropertyChanged
	{
		public ICommand GetLocation { get; private set; }
		double Lat, Long;
		public AddNewVenueViewModel()
		{
			GetLocation = new Command<string>(GetLoc);
		}


		public void GetLoc(string temp)
		{

		}
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Ons the property changed.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		void OnPropertyChanged(string propertyName)
		{
			//PropertyChangedEventHandler eventHandler = this.PropertyChanged;
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

			}
		}
	}
}
