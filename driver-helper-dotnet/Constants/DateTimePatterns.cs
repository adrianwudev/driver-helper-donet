using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace driver_helper_dotnet.Constants
{
    public class DateTimePatterns
    {
        private readonly string _datePattern = @"^\d{4}/\d{2}/\d{2}（[一二三四五六日]）$";
        private readonly string _hourMinPattern = @"^\d{2}:\d{2}";

        public string DatePattern { get { return _datePattern; } }
        public string HourMinPattern { get { return _hourMinPattern; } }
    }
}
