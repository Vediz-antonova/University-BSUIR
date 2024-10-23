using System;
using System.Collections.Generic;

namespace _353504_Antonava_Lab3.Entities
{
	public class Journal
	{
		private List<string> events = new List<string>();

		public void LogEvent(string description)
		{
			events.Add(description);
		}

		public void DisplayEvents()
		{
			foreach (var eventDescription in events)
			{
				Console.WriteLine(eventDescription);
			}
		}
	}
}