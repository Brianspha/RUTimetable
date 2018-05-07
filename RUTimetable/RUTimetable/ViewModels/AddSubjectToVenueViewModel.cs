using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace RUTimetable.ViewModels
{
	public class AddSubjectToVenueViewModel : INotifyPropertyChanged
	{
		public ICommand AddToVenue { get; private set; }
		public int SelectedIndex { get; set; }
		public string SelectedVenue { get; set; }

		RealmDataBase db;
		public List<string> Venues;
		public List<string> venues
		{
			set
			{
				Venues = value.ToList();
				OnPropertyChanged(nameof(Venues));

			}
			get
			{
				return Venues;
			}
		}
		public int Selected
		{
			get
			{
				return SelectedIndex;
			}

			set
			{
				if (SelectedIndex != value)
				{
					SelectedIndex = value;
					SelectedVenue = Venues[SelectedIndex];
					OnPropertyChanged(nameof(SelectedIndex));
				}
			}
		}

		public AddSubjectToVenueViewModel()
		{
			AddToVenue = new Command<string>(AddSubjectToVenue);
			db = new RealmDataBase();
		}
		public void GetLoc(string temp)
		{

		}
		public void AddSubjectToVenue(string subject)
		{
			GetVenues();
			db.AddSubjectToVenue(SelectedVenue,subject);
		}
		public void GetVenues()
		{
			Venues = new RealmDataBase().GetVenues();
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
