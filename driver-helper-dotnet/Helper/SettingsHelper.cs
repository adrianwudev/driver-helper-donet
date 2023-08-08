using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace driver_helper_dotnet.Helper
{
    internal class SettingsHelper
    {
        private string connStr { get; set; }
        public SettingsHelper()
        {
            string jsonFilePath = "appSettings.json";
            string jsonContent = File.ReadAllText(jsonFilePath);
            var jsonObject = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonContent);
            if (jsonObject.TryGetValue("ConnectionStrings", out Dictionary<string, string> connStrMap) &&
                    connStrMap is Dictionary<string, string> connStrDict )
            {
                if (connStrDict.TryGetValue("PostgreSQLConnection", out string connStr))
                    this.connStr = connStr;
            }
        }

        public string GetConnectionString()
        {
            return this.connStr;
        }
        
    }
}
