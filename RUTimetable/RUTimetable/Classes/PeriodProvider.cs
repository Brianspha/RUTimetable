using System;
using System.Collections.Generic;
using System.Linq;
using Realms;
namespace RUTimetable
{
	public class PeriodProvider
	{
		
		Realm db;
		public PeriodProvider()
		{
			db = Realm.GetInstance(); //Get default realm database for Appm
		}

	}
}
