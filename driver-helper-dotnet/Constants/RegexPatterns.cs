using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace driver_helper_dotnet.Constants
{
    public class RegexPatterns
    {
        List<string> AddressPatterns = new List<string>()
        {
            @"上🚘：(.*?)(?=\r\n|$)",
        };

        List<string> DropoffPatterns = new List<string>()
        {
            @"下🚘：(.*?)(?=\r\n|$)",
        };

        List<string> TimePatterns = new List<string>()
        {
            @"時間：(\d{2}:\d{2})",
        };

        List<string> CityPatterns = new List<string>()
        {
            @"([\p{IsCJKUnifiedIdeographs}\p{IsCJKCompatibilityIdeographs}\p{IsCJKUnifiedIdeographsExtensionA}]+市)",
        };

        List<string> DistrictPatterns = new List<string>()
        {
            @"[^市縣]+區",
        };
    }
}
