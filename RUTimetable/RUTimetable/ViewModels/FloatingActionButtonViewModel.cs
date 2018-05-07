using Refractored.FabControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Notifications;
using Rg.Plugins.Popup.Extensions;

namespace RUTimetable.ViewModels
{
    public class FloatingActionButtonViewModel
    {
        private readonly FloatingActionButtonView fab;
        private AbsoluteLayout absolute;
		INavigation Nav;
		bool AddedAlready = false;
        public FloatingActionButtonViewModel()
        {
			fab = new FloatingActionButtonView()
			{
				ImageName = "back",
				ColorNormal = Color.CadetBlue,
				ColorPressed = Color.WhiteSmoke,
				ColorRipple = Color.LightBlue,
				Clicked = async (sender, args) =>
				{
					fab.Hide();
					//var masterPage = pg;
					await Task.Delay(1500);
                   
                    fab.Show();
					//await masterPage.Navigation.PushAsync(new NavigationPage(pg));
				},
				IsEnabled = true,
				IsVisible=true,
				IsPlatformEnabled=true
            };
             absolute = new AbsoluteLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
          
        }
        public FloatingActionButtonViewModel(INavigation nav)
        {
            fab = new FloatingActionButtonView()
            {
                ImageName = "plus",
                ColorNormal = Color.CadetBlue,
				ColorPressed = Color.White,
                ColorRipple = Color.LightBlue,
                Clicked = async (sender, args) =>
                {
                    fab.Hide();
                    //var masterPage = pg;
                    await Task.Delay(1500);
                    await nav.PushPopupAsync(new AddNewSubjectPopUp());
                    fab.Show();
                    //await masterPage.Navigation.PushAsync(new NavigationPage(pg));
                },
                IsEnabled = true,
                IsVisible = true,
                IsPlatformEnabled = true
            };
            absolute = new AbsoluteLayout()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
			Nav = nav;
        }
        public void AddMapToAbsouluteLayOut(CustomMap customMap)
        {
            // Position the pageLayout to fill the entire screen.
            // Manage positioning of child elements on the page by editing the pageLayout.
            AbsoluteLayout.SetLayoutFlags(customMap, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(customMap, new Rectangle(0f, 0f, 1f, 1f));
            absolute.Children.Add(customMap);
            // Overlay the FAB in the bottom-right corner
			AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            absolute.Children.Add(fab);
        }
        public AbsoluteLayout GetLayOut()
        {
            return absolute;
        }
		public void AddDayLayOutToAbsoluteLayOut(ListView list)
		{
			if (AddedAlready) return;
            if (absolute.Children.Count == 2) absolute.Children.RemoveAt(1);
			// Position the pageLayout to fill the entire screen.
			// Manage positioning of child elements on the page by editing the pageLayout.
			AbsoluteLayout.SetLayoutFlags(list, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(list, new Rectangle(0f, 0f, 1f, 1f));
			absolute.Children.Add(list);
			// Overlay the FAB in the bottom-right corner
			AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.YProportional);
			AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			absolute.Children.Add(fab);
			AddedAlready = true;
		}
        public void AddContentPageToAbsoluteLayOut(StackLayout Page)
        {
			if (AddedAlready) return;
            if (absolute.Children.Count == 2) absolute.Children.RemoveAt(1);
            // Position the pageLayout to fill the entire screen.
            // Manage positioning of child elements on the page by editing the pageLayout.
            AbsoluteLayout.SetLayoutFlags(Page, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(Page, new Rectangle(0f, 0f, 1f, 1f));
            absolute.Children.Add(Page);
            // Overlay the FAB in the bottom-right corner
            AbsoluteLayout.SetLayoutFlags(fab, AbsoluteLayoutFlags.YProportional);
            AbsoluteLayout.SetLayoutBounds(fab, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            absolute.Children.Add(fab);
        }
    }
}
