using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace driver_helper_dotnet.Helper
{
    public class DateHelper
    {
        public DateTime GetDateFromTxt(string date)
        {
            string dateFormat = "yyyy/MM/dd";
            DateTime parsedDate = DateTime.ParseExact(date.Substring(0, 10), dateFormat, CultureInfo.InvariantCulture);
            return parsedDate;
        }

        internal DateTime GetHourMinFromTxt(string date)
        {
            string timeString = date.Split('\t')[0];
            DateTime.TryParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime lineHourMin);
            return lineHourMin;
        }
    }
}
