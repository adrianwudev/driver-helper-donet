using driver_helper_dotnet.Constants;
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
        private readonly string datePattern = @"^\d{4}/\d{2}/\d{2}（[一二三四五六日]）$";
        private readonly string hourMinPattern = @"^\d{2}:\d{2}";

        private readonly RegexPatterns regexPatterns = new RegexPatterns();
        private readonly MatchHelper matchHelper = new MatchHelper();

        public FilterHelper()
        {
            dateHelper = new DateHelper();
        }
        public List<Order> GetOrdersByFilter(string[] lines, string groupName, CancellationToken cancellationToken)
        {
            bool isAddressMatched = false;
            bool isTimeMatched = false;
            Match addressMatch = null;
            Match timeMatch = null;
            int currentLine = 0;

            DateTime todayDateTime = DateTime.MinValue;
            DateTime lineDateTIme = DateTime.MinValue;

            List<Order> orders = new List<Order>();
            Order order = new Order();

            foreach (string line in lines)
            {
                //Thread.Sleep(1);

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                currentLine += 1;
                if (currentLine >= 500)
                {
                    View.FormView.CurrentLine += currentLine;
                    currentLine = 0;
                    Thread.Sleep(1);
                }
                

                
                todayDateTime = SetDay(datePattern, todayDateTime, line);
                SetLineDateTime(hourMinPattern, todayDateTime, ref lineDateTIme, line);


                if (!isAddressMatched)
                    addressMatch = matchHelper.RegexMatch(line, regexPatterns.AddressPatterns);
                if (!isTimeMatched)
                    timeMatch = matchHelper.RegexMatch(line, regexPatterns.TimePatterns);
                Match dropoffAddressMatch = matchHelper.RegexMatch(line, regexPatterns.DropoffPatterns);


                if (addressMatch.Success)
                {
                    isAddressMatched = true;
                    InitOrder(groupName, lineDateTIme, order);

                    string pickupAddress = addressMatch.Groups[1].Value.Trim().Replace("：", "");
                    order.Address = pickupAddress;

                    // City
                    Match cityMatch = matchHelper.RegexMatch(order.Address, regexPatterns.CityPatterns);
                    if (cityMatch.Success)
                    {
                        order.City = cityMatch.Groups[1].Value.Trim();
                    }

                    // District
                    Match districtMatch = matchHelper.RegexMatch(order.Address, regexPatterns.DistrictPatterns);
                    if (districtMatch.Success)
                    {
                        order.District = districtMatch.Groups[0].Value.Trim();
                    }
                }


                // PickupTime
                if (timeMatch.Success)
                {
                    isTimeMatched = true;
                    DateTime pickupTime = dateHelper.GetHourMinFromTxt(timeMatch.Groups[1].Value);
                    order.PickUpTime = todayDateTime.Date + pickupTime.TimeOfDay;
                }

                if (dropoffAddressMatch.Success)
                {
                    // Dropoff Address 
                    string dropoffAddress = dropoffAddressMatch.Groups[1].Value.Trim();
                    order.PickUpDrop = dropoffAddress;

                    order.IsException = !checkOrderValid(order);

                    SetOrderBeforeAdd(groupName, lineDateTIme, order);
                    orders.Add(order);

                    // Reset
                    order = new Order();
                    isAddressMatched = false;
                    isTimeMatched = false;
                }

                Debug.WriteLine(line);
            }


            return orders;
        }

        private static void InitOrder(string groupName, DateTime lineDateTIme, Order order)
        {
            order.PickUpTime = null;
        }

        private static void SetOrderBeforeAdd(string groupName, DateTime lineDateTIme, Order order)
        {
            order.GroupName = groupName;
            order.OrderTime = lineDateTIme;
            order.Weekday = lineDateTIme.DayOfWeek.ToString().ToUpper();
            order.CreateTime = DateTime.Now;
            order.ModifyTime = DateTime.Now;
        }

        private bool checkOrderValid(Order order)
        {
            return order.City != null && order.District != null;
        }

        private void SetLineDateTime(string HourMinPattern, DateTime todayDateTime, ref DateTime lineDateTIme, string line)
        {
            if (Regex.IsMatch(line, HourMinPattern))
            {
                var lineHourMin = dateHelper.GetHourMinFromTxt(line);
                lineDateTIme = todayDateTime.Date + lineHourMin.TimeOfDay;
            }
        }

        private DateTime SetDay(string datePattern, DateTime todayDateTime, string line)
        {
            if (Regex.IsMatch(line, datePattern))
            {
                todayDateTime = dateHelper.GetDateFromTxt(line);
            }

            return todayDateTime;
        }
    }
}
