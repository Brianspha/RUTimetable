using System;
using System.Windows.Input;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace RUTimetable
{
	public class AddTimetableViewModel
	{
		public ICommand addTimetable { get; private set; }
		public AddTimetableViewModel()
		{
			addTimetable = new Command<string>(AddTimetable);
		}
		public async void AddTimetable(string StudentNumber)
		{
				var studentNumber = StudentNumber;
				if (!string.IsNullOrEmpty(studentNumber))
				{
					var test = UserDialogs.Instance;
					try
					{
						TimeTableGetter getter = new TimeTableGetter(studentNumber);

					}
					catch (Exception E)
					{
						await test.AlertAsync(E.Message, "Error", "OK");
					}
						//	loader.IsRunning = false;
					}
				else
				{
					await UserDialogs.Instance.AlertAsync("Student number cannot be empty", "Student Number", "OK");
				}
		}
	}
}
