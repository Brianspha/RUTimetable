using System;
using Xamarin.Forms;
using System.ComponentModel;
using System.Threading.Tasks;

namespace RUTimetable
{
	public class CustomActivityIndicatorViewModel : INotifyPropertyChanged
	{
		public bool IsLoading { get; set; }
		private bool busy = false;

		public bool IsBusy
		{
			get { return busy; }
			set
			{
				if (busy == value)
					return;

				busy = value;
				OnPropertyChanged("IsBusy");
			}
		}


		public async Task GetTimeTable()
		{
			if (IsBusy)
				return;


			try
			{
				IsBusy = true;

				//do stuff here that is going to take a while

			}
			finally
			{
				IsBusy = false;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string name)
		{
			var changed = PropertyChanged;
			if (changed == null)
				return;

			changed(this, new PropertyChangedEventArgs(name));


		}

	}
}
