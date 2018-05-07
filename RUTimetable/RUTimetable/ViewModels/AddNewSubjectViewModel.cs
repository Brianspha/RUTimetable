using Acr.UserDialogs;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RUTimetable.ViewModels
{
    public class AddNewSubjectViewModel : INotifyPropertyChanged
    {
        public INavigation navigation { get; set; }
        public string day { get; set; }
        public int semester { get; set; }
        public int period { get; set; }
        public ICommand Save { get; set; }
        public ICommand PeriodSelectedAction { get; set; }
        public ICommand SemesterSelectedAction { get; set; }
        public ICommand Close { get; set; }

        public List<string> days { get; set; }
        public List<string> Days
        {
            get
            {
                return days;
            }
            set
            {
                if (Days!=value)
                {
                    days = MiscellaneousProvider.Days.ToList();
                    OnPropertyChanged(nameof(Days));
                }
            }

        }
        public List<int> periods { get; set; }
        public List<int> Periods
        {
            get
            {
                return periods;
            }
            set
            {
                if (Periods != value)
                {
                    periods = MiscellaneousProvider.PeriodProvider.ToList();
                    OnPropertyChanged(nameof(Periods));
                }
            }

        }
        public List<int> semesters { get; set; }
        public List<int> Semesters
        {
            get
            {
                return semesters;
            }
            set
            {
                if (semesters !=value)
                {
                    periods = MiscellaneousProvider.SemesterProvider.ToList();
                    OnPropertyChanged(nameof(semesters));
                }
            }

        }
        public int Semester
        {
            get
            {
                return semester;
            }
            set
            {
                if (Semester !=value)
                {
                    semester = semesters[semester];
                    OnPropertyChanged(nameof(Semester));
                }
            }
        }
        public int Period
        {
            get
            {
                return period;
            }
            set
            {
                if (Period!= value)
                {
                    period = periods[period];
                    OnPropertyChanged(nameof(Period));
                }
            }
        }
        public int SelectedDayIndex {

            get {

                return SelectedDayIndex;
            }

            set {
                if (SelectedDayIndex != value)
                {
                    SelectedDayIndex = value;
                    OnPropertyChanged(nameof(Day));
                    OnPropertyChanged(nameof(SelectedDayIndex));
                }
            }

        }
        public string Day
        {
            get
            {
                return day;
            }
            set
            {
                day = days[SelectedDayIndex];
            }
        }

        public AddNewSubjectViewModel(INavigation nav)
        {
            navigation = nav;
        }
        void SetUp()
        {
            SelectedDayIndex = 0;
            Period = 0;
            Semester = 0;
            Save = new Command<string>(Save_Clicked);
            Close = new Command(OnClose);
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
        void Save_Clicked(string newSubjectName)
        {
            RealmDataBase db = new RealmDataBase();
            var student = db.GetStudent();
            if (student != null && !string.IsNullOrEmpty(newSubjectName) && !string.IsNullOrEmpty(day) && period > 0 && semester > 0)
            {
                UserDialogs.Instance.ShowLoading("Saving Please Wait", MaskType.Black);
                db.Write(newSubjectName, period.ToString(), new TimeProvider().GetPeriodTime(period.ToString()), semester, day);
                UserDialogs.Instance.HideLoading();

            }

            else
            {
                UserDialogs.Instance.Alert("Nigga is you dumb? fill all the required information yesses!!", "Ëmpty subject", "OK");
            }
        }
        private async void OnClose()
        {
            await navigation.PopPopupAsync();
        }
    }
}
