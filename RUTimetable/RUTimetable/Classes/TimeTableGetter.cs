using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using Timetable.Exception;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Acr.UserDialogs;
namespace RUTimetable
{
	/// <summary>
	/// Time table getter.
	/// </summary>
	public class TimeTableGetter
	{
		RealmDataBase db;
		public bool IsBusy = false;
		public TimeTableGetter(string studentNumber)
		{
			db = new RealmDataBase();
			try
			{
				IntermediateCall(studentNumber);
			}
			catch (TimeTableException e)
			{
				UserDialogs.Instance.Alert(e.Message);
			}
		}
		public async void IntermediateCall(string s)
		{
			await GetTimetable(s);
		}
		/// <summary>
		/// Gets the time table with venues.
		/// This will be used to supply data to the Google maps
		/// </summary>
		/// <returns>The time table with venues.</returns>
		/// tp
		public async void GetTimeTableWithVenues()
		{ 
			var client = new HttpClient();
			string URL = "https://scifac.ru.ac.za/wwwtime/timetable.php";
			var values = new List<KeyValuePair<string, string>>();
			TimeTableParser temp = new TimeTableParser();
			var acc =temp.GetAcronyms();
            for (int i = 0; i < acc.Count; i++)
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("mnemonic[]", acc.Count>0?acc[i]:"")
                });
                var myHttpClient = new HttpClient();
                var responsefromserver = await myHttpClient.PostAsync(URL, formContent);
                client.Dispose();
                var stringContent = await responsefromserver.Content.ReadAsStringAsync();
                var html = new HtmlDocument();
                html.LoadHtml(stringContent);
                var root = html.DocumentNode;
                var nodes = root.Descendants();
                var totalNodes = nodes.Count();
                var table = root.Descendants("table").ToArray();
                // Check the format returned by the website 
                //Got no internet connection right now will check later
            }
		}
		/// <summary>
		/// Gets the timnetable from the time table website this is the brain of the App everything depends of on this fetching the data correctly.
		/// </summary>
		/// <param name="g"> String g is the student No passed in from the AddTimetable Page</tb>.</param>
		public async Task GetTimetable(string g)
		{				
			UserDialogs.Instance.ShowLoading("Fetching Timetable Please wait!", MaskType.Black);
            var client = new HttpClient();
			string URL = "https://scifac.ru.ac.za/timetable/personal/lookup.php";
			var formContent = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("studentnumber", g)
			});
			IsBusy = true;
			var myHttpClient = new HttpClient();
			try
			{
				var responsefromserver = await myHttpClient.PostAsync(URL, formContent);
				//byte[] responseBytes = webClient.UploadValues(URL, "POST", formData);
				//string responsefromserver = Encoding.UTF8.GetString(responseBytes, 0, responseBytes.Length);
				//   textBox1.Text = responsefromserver;
				client.Dispose();
				var stringContent = await responsefromserver.Content.ReadAsStringAsync();
				var html = new HtmlDocument();
				html.LoadHtml(stringContent);
				var root = html.DocumentNode;
				var nodes = root.Descendants();
				var totalNodes = nodes.Count();
				//var Heading = root.Descendants("h1").ToArray()[1].InnerText;
				object[] arr = root.Descendants("table").ToArray();//check to see if the student is registered or not
				if (arr.Length == 0)
				{
					UserDialogs.Instance.Alert("Time table is currently unavailable please try again in a few days or later on today");
					UserDialogs.Instance.HideLoading();
					return;
				}
				//If student used surname instead of Student Number
				if (root.Descendants("table").ToArray().Length > 1)
				{
					UserDialogs.Instance.HideLoading();
					UserDialogs.Instance.Alert("Please use your student number instead of your Surname");
					return;
				}
				else
				{
					var table = root.Descendants("table").ToArray()[0].InnerText;
					var Parser = new TimeTableParser(table);
					Student student = new Student();
					student = db.StoreTimetableToDataBase(Parser.GetSemester(1), 1, student);
					db.StoreTimetableToDataBase(Parser.GetSemester(2), 2, student);
					GetTimeTableWithVenues();
					UserDialogs.Instance.HideLoading();
				}
			}
			catch (Exception e)
			{
				UserDialogs.Instance.Alert("Something went wrong while fetching the timetable please ensure your connected to the internet", "Connection error!", "OK");
                UserDialogs.Instance.HideLoading();
 			
			}
		}
        /// <summary>
        /// Checks to see if student has inserted their surname instead of their
        /// Student Number this can cause issues for instance students having simalar surnames
        /// This way i can limit the app to just using student numbers instead of names
        /// </summary>
        /// <param name="studentNumber"></param>
        /// <returns>true if student is using student number ,false if using surname</returns>
        private bool CheckifSurnameSubmittedInsteadOfStudentNumber(string studentNumber)
        {
            bool Good = true; 
            var list =studentNumber.ToCharArray();
            int letterCountMax = 2,DigitCountMax=6; // student number has two characters and 6 digits i.e g14m1190
            for(int i=0; i < list.Length; i++)
            {
                if (Char.IsLetter(list[i])) letterCountMax--;
                else if (Char.IsDigit(list[i])) DigitCountMax--;
            }
            if (letterCountMax == 0 && DigitCountMax == 0) return true; //student is using student number and not surname
            else return false;
        }
    }
}
