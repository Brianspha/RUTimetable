using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Threading.Tasks;
using RUTimetable.ViewModels;

namespace RUTimetable
{
	public partial class UpdateSubjectTaken : ContentPage
	{
		int semester = 0,period=0;
		string day = "";
        AddNewSubjectViewModel view;
		public UpdateSubjectTaken()
		{
			InitializeComponent();
            view = new AddNewSubjectViewModel(Navigation);
            BindingContext = view;


        }

    }
}
