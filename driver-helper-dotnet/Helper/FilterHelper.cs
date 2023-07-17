using driver_helper_dotnet.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace driver_helper_dotnet.Helper
{
    public class FilterHelper
    {
        private DateHelper dateHelper;

        public FilterHelper()
        {
            dateHelper = new DateHelper();
        }
        public void GetOrdersByFilter(string[] lines)
        {
            string datePattern = @"^\d{4}/\d{2}/\d{2}（[一二三四五六日]）$";
            string HourMinPattern = @"^\d{2}:\d{2}";
            DateTime todayDateTime = DateTime.MinValue;
            DateTime lineHourMin = DateTime.MinValue;
            DateTime lineDateTIme = DateTime.MinValue;

            string addressPattern = @"上🚘：(.*?)(?=\r\n|$)";
            string dropoffAddressPattern = @"下🚘：(.*?)(?=\r\n|$)";
            string timePattern = @"時間：(\d{2}:\d{2})";
            string cityPattern = @"([\p{IsCJKUnifiedIdeographs}\p{IsCJKCompatibilityIdeographs}\p{IsCJKUnifiedIdeographsExtensionA}]+市)";
            string districtPattern = @"[^市縣]+區";


            List<Order> orders = new List<Order>();
            Order order = new Order();

            foreach (string line in lines)
            {
                todayDateTime = SetDay(datePattern, todayDateTime, line);
                SetLineDateTime(HourMinPattern, todayDateTime, ref lineHourMin, ref lineDateTIme, line);

                Match addressMatch = Regex.Match(line, addressPattern);
                Match dropoffAddressMatch = Regex.Match(line, dropoffAddressPattern);
                Match timeMatch = Regex.Match(line, timePattern);

                if (addressMatch.Success)
                {
                    string pickupAddress = addressMatch.Groups[1].Value.Trim();
                    order.Address = pickupAddress;
                    Debug.WriteLine("上車地址： " + pickupAddress);
                    // City
                    Match cityMatch = Regex.Match(order.Address, cityPattern);
                    if (cityMatch.Success)
                    {
                        order.City = cityMatch.Groups[1].Value.Trim();
                        Debug.WriteLine("城市： " + order.City);
                    }
                    // District
                    Match districtMatch = Regex.Match(order.Address, districtPattern);
                    if (districtMatch.Success)
                    {
                        order.District = districtMatch.Groups[0].Value.Trim();
                        Debug.WriteLine("區： " + order.District);
                    }


                    order.OrderTime = lineDateTIme;
                }
                if (dropoffAddressMatch.Success)
                {
                    string dropoffAddress = dropoffAddressMatch.Groups[1].Value.Trim();
                    order.PickUpDrop = dropoffAddress;
                    Debug.WriteLine("下車地址： " + dropoffAddress);

                    order.CreateTime = DateTime.Now;
                    order.ModifyTime = DateTime.Now;
                    orders.Add(order);
                    order = new Order();
                }

                Debug.WriteLine(line);

            }
        }

        private void SetLineDateTime(string HourMinPattern, DateTime todayDateTime, ref DateTime lineHourMin, ref DateTime lineDateTIme, string line)
        {
            if (Regex.IsMatch(line, HourMinPattern))
            {
                lineHourMin = dateHelper.GetHourMinFromTxt(line);
                lineDateTIme = todayDateTime.Date + lineHourMin.TimeOfDay;
            }
        }

        private DateTime SetDay(string datePattern, DateTime todayDateTime, string line)
        {
            if (Regex.IsMatch(line, datePattern))
            {
                todayDateTime = dateHelper.GetDateFromTxt(line);
                Debug.WriteLine(todayDateTime);
            }

            return todayDateTime;
        }
    }
}
