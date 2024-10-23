using System;
using System.Globalization;

namespace Services
{
    public static class DateServices
    {
        public static string GetDay(string date)
        {
            DateTime parsedDate;
            if (DateTime.TryParse(date, out parsedDate))
            {
                return parsedDate.ToString("dddd", new CultureInfo("ru-RU"));
            }
            else
            {
                return "Некорректная дата";
            }
        }

        public static int GetDaysSpan(int day, int month, int year)
        {
            DateTime currentDate = DateTime.Now;
            DateTime futureDate;
            try
            {
                futureDate = new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                return -1;
            }

            if (futureDate < currentDate)
            {
                return -1;
            }
            else
            {
                return (futureDate - currentDate).Days;
            }
        }
    }
}