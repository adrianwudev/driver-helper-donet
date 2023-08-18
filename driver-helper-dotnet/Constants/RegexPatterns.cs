using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace driver_helper_dotnet.Constants
{
    public class RegexPatterns
    {
        public List<string> AddressPatterns { get; private set; }
        public List<string> DropoffPatterns { get; private set; }
        public List<string> TimePatterns { get; private set; }
        public List<string> CityPatterns { get; private set; }
        public List<string> DistrictPatterns { get; private set; }
        public DateTimePatterns DateTimePatterns { get; private set; }

        public RegexPatterns()
        {
            LoadPatternsFromJson();
            DateTimePatterns = new DateTimePatterns();
        }

        private void LoadPatternsFromJson()
        {
            string jsonFilePath = "patternsSettings.json";

            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                var patterns = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonContent);

                AddressPatterns = patterns["AddressPatterns"];
                DropoffPatterns = patterns["DropoffPatterns"];
                TimePatterns = patterns["TimePatterns"];
                CityPatterns = patterns["CityPatterns"];
                DistrictPatterns = patterns["DistrictPatterns"];
            }
            else
            {
                throw new Exception("the JSON file doesn't exist");
            }
        }
    }
}
