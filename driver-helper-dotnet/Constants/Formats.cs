using driver_helper_dotnet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace driver_helper_dotnet.Constants
{
    public class Formats
    {
        public static readonly Dictionary<string, string> FormatDict = new Dictionary<string, string>()
        {
            { "日期：", nameof(OrderFormats.PickUpTimeDate) },
            { "時間：", nameof(OrderFormats.PickUpTime) },
            { "上車●",  nameof(OrderFormats.AddressUp) },
            { "目地：", nameof(OrderFormats.AddressOff) }
        };

        public class OrderFormats
        {
            public string PickUpTimeDate { get; set; }
            public string PickUpTime { get; set; }
            public string AddressUp { get; set; }
            public string AddressOff { get; set; }
        }
    }
}
