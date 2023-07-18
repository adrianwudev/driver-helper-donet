using driver_helper_dotnet.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace driver_helper_dotnet.Helper
{
    public class MatchHelper
    {
        public Match RegexMatch(string line, List<string> regexPatterns)
        {
            foreach(var pattern in regexPatterns)
            {
                Match match = Regex.Match(line, pattern);
                if (match.Success)
                    return match;
            }
            return Match.Empty;
        }
    }
}
