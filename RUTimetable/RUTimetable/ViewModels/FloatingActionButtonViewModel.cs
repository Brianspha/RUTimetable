using Refractored.FabControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Notifications;
using Rg.Plugins.Popup.Extensions;
using Syncfusion.SfRadialMenu.XForms;
using RUTimetable.Classes;

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
            // Overlay the radialmenue in the bottom-right corner
            var rad = SetupRadialMenu();
            AbsoluteLayout.SetLayoutFlags(rad, AbsoluteLayoutFlags.YProportional);
            AbsoluteLayout.SetLayoutBounds(rad, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            absolute.Children.Add(rad);
        }
        public AbsoluteLayout GetLayOut()
        {
            return absolute;
        }
		public void AddDayLayOutToAbsoluteLayOut(Syncfusion.ListView.XForms.SfListView list)
		{
			if (AddedAlready) return;
            if (absolute.Children.Count == 2) absolute.Children.RemoveAt(1);
			// Position the pageLayout to fill the entire screen.
			// Manage positioning of child elements on the page by editing the pageLayout.
			AbsoluteLayout.SetLayoutFlags(list, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(list, new Rectangle(0f, 0f, 1f, 1f));
			absolute.Children.Add(list);
            // Overlay the radialmenue in the bottom-right corner
            var rad = SetupRadialMenu();
            AbsoluteLayout.SetLayoutFlags(rad, AbsoluteLayoutFlags.YProportional);
            AbsoluteLayout.SetLayoutBounds(rad, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            absolute.Children.Add(rad);
            AddedAlready = true;
		}
        /// <summary>
        /// Creates a new Radial Menu to be placed in every tab
        /// </summary>
        public SfRadialMenu SetupRadialMenu()
        {
            //Creating instance for RadialMenu
            SfRadialMenu radialMenu = new SfRadialMenu();
            radialMenu.IsDragEnabled = true;
            //Initializing RadialMenu's properties
            radialMenu.CenterButtonText = "\uE706";
            radialMenu.CenterButtonBackText = "\uE707";
            radialMenu.CenterButtonFontFamily = "RUTimetable.ttf";
            radialMenu.CenterButtonBackFontFamily = "RUTimetable.ttf";
            radialMenu.CenterButtonRadius = 32;
            radialMenu.CenterButtonBorderColor = Color.White;

            //Adding RadialMenu items

            string[] Names = new string[] { "\uE705", "\uE709", "\uE704", "\uE711" };
            string[] semester = new string[] { "\uE701", "\uE702" };//ONE AND TWO
            string[] campusMap = new string[] { "\uE710", "\uE708" };//CAMPUS MAP ,ADD NEW VENUE
            string[] settings = new string[] { "\uE700" };//MODIFY Default settings
            string[] NewSub = new string[] { "\uE709" };//new subject option

            //Adding radialMenu main menu items
            for (int i = 0; i < 4; i++)
            {
                SfRadialMenuItem mainMenuItems = new SfRadialMenuItem();
                mainMenuItems.IconFontSize = 22;
                mainMenuItems.FontIconText = Names[i];
                mainMenuItems.ItemHeight = 30;
                mainMenuItems.ItemWidth = 40;
                mainMenuItems.TextColor = Color.White;
                mainMenuItems.ItemTapped += Handle_ItemTapped;
              //  mainMenuItems.Text =Names[i];
                radialMenu.Items.Add(mainMenuItems);
            }
            // Adding Semester items  submenu items
            for (int i = 0; i < semester.Length; i++)
            {
                SfRadialMenuItem semesterSubItems = new SfRadialMenuItem();
                semesterSubItems.IconFontSize = 20;
                semesterSubItems.FontIconText = semester[i];
                semesterSubItems.ItemHeight = 30;
                semesterSubItems.ItemWidth = 40;
                semesterSubItems.TextColor = Color.White;
                semesterSubItems.ItemTapped += Handle_ItemTapped;
                radialMenu.Items[1].Items.Add(semesterSubItems);
            }
            for (int i = 0; i < campusMap.Length; i++)
            {
                SfRadialMenuItem CampusMapSubItems = new SfRadialMenuItem();
                CampusMapSubItems.IconFontSize = 20;
                CampusMapSubItems.FontIconText = campusMap[i];
                CampusMapSubItems.ItemHeight = 30;
                CampusMapSubItems.ItemWidth = 40;
                CampusMapSubItems.TextColor = Color.White;
                CampusMapSubItems.ItemTapped += Handle_ItemTapped;
                CampusMapSubItems.IconFontFamily = "RUTimetable.ttf";
                radialMenu.Items[3].Items.Add(CampusMapSubItems);
            }
            ////for (int i = 0; i < settings.Length; i++)
            ////{
            //    SfRadialMenuItem settingsSubItems = new SfRadialMenuItem();
            //    settingsSubItems.IconFontSize = 20;
            //    settingsSubItems.FontIconText = semester[i];
            //    settingsSubItems.ItemHeight = 30;
            //    settingsSubItems.ItemWidth = 40;
            //    settingsSubItems.TextColor = Color.White;
            //    settingsSubItems.ItemTapped += Handle_ItemTapped;
            //    radialMenu.Items[0].Items.Add(settingsSubItems);
            ////}


            return radialMenu;
        }
        /// <summary>
        /// Handles Radial Menu item taps
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Handle_ItemTapped(object sender, Syncfusion.SfRadialMenu.XForms.ItemTappedEventArgs e)
        {
            Acr.UserDialogs.UserDialogs.Instance.Alert("Clicked on " + ((Syncfusion.SfRadialMenu.XForms.SfRadialMenuItem)sender).Text);
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
            // Overlay the radialmenue in the bottom-right corner
            var rad = SetupRadialMenu();
            AbsoluteLayout.SetLayoutFlags(rad, AbsoluteLayoutFlags.YProportional);
            AbsoluteLayout.SetLayoutBounds(rad, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            absolute.Children.Add(rad);
        }
    }
}
