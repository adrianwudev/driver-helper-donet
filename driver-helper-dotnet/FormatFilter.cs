using driver_helper_dotnet.Constants;
using driver_helper_dotnet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static driver_helper_dotnet.Constants.Formats;

namespace driver_helper_dotnet
{
    public class FormatFilter
    {
        public OrderFormats GetByFormatFilter(string text)
        {
            OrderFormats orderFormat = new OrderFormats();

            foreach (var format in Formats.FormatDict)
            {
                int startIndex = text.IndexOf(format.Key);
                if (startIndex != -1)
                {
                    startIndex += format.Key.Length;
                    int endIndex = text.IndexOf('\n', startIndex);
                    if (endIndex != -1)
                    {
                        string value = text.Substring(startIndex, endIndex - startIndex).Trim();
                        typeof(OrderFormats).GetProperty(format.Value)?.SetValue(orderFormat, value);
                    }
                }
            }

            return orderFormat;
        }
    }
}
