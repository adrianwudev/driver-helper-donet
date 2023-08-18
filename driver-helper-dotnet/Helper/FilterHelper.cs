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
        private readonly DateHelper dateHelper;
        private readonly RegexPatterns regexPatterns;
        private readonly MatchHelper matchHelper;
        private readonly SettingsHelper settingsHelper;

        public FilterHelper()
        {
            dateHelper = new DateHelper();
            regexPatterns = new RegexPatterns();
            matchHelper = new MatchHelper();
            settingsHelper = new SettingsHelper();
        }
        public List<Order> GetOrdersByFilter(string[] lines, string groupName, CancellationToken cancellationToken)
        {
            bool isAddressMatched = false;
            bool isTimeMatched = false;
            Match addressMatch = null;
            Match timeMatch = null;
            int currentLine = 0;
            int scanLimit = settingsHelper.GetOrderSize();
            int scanCount = 0;

            DateTime todayDateTime = DateTime.MinValue;
            DateTime lineDateTime = DateTime.MinValue;

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
                

                
                todayDateTime = SetDay(todayDateTime, line);
                SetLineDateTime(todayDateTime, ref lineDateTime, line);

                if (!isAddressMatched)
                    addressMatch = matchHelper.RegexMatch(line, regexPatterns.AddressPatterns);
                if (!isTimeMatched)
                    timeMatch = matchHelper.RegexMatch(line, regexPatterns.TimePatterns);
                Match dropoffAddressMatch = matchHelper.RegexMatch(line, regexPatterns.DropoffPatterns);


                if (addressMatch.Success)
                {
                    isAddressMatched = true;
                    // reset scan count
                    scanCount = 0;
                    InitOrder(groupName, lineDateTime, order);

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

                    continue;
                }


                // PickupTime
                if (timeMatch.Success)
                {
                    isTimeMatched = true;
                    DateTime pickupTime = dateHelper.GetHourMinFromTxt(timeMatch.Groups[1].Value);
                    order.PickUpTime = todayDateTime.Date + pickupTime.TimeOfDay;

                    continue;
                }

                if (dropoffAddressMatch.Success)
                {
                    // Is pickUpAddress empty
                    if (string.IsNullOrWhiteSpace(order.Address))
                    {
                        order.Address = "此單找不到上車地點";
                        SetOrderTime(lineDateTime, order);
                    }
                    // Dropoff Address 
                    string dropoffAddress = dropoffAddressMatch.Groups[1].Value.Trim();
                    order.PickUpDrop = dropoffAddress;

                    order.IsException = !checkOrderValid(order);

                    SetOrderBeforeAdd(groupName, lineDateTime, order);
                    orders.Add(order);

                    // Reset
                    order = new Order();
                    isAddressMatched = false;
                    isTimeMatched = false;

                    continue;
                }


                Debug.WriteLine(line);
            }


            return orders;
        }

        private static void InitOrder(string groupName, DateTime lineDateTime, Order order)
        {
            order.PickUpTime = null;
            SetOrderTime(lineDateTime, order);
        }

        private static void SetOrderTime(DateTime lineDateTime, Order order)
        {
            order.OrderTime = lineDateTime;
            order.Weekday = lineDateTime.DayOfWeek.ToString().ToUpper();
        }

        private static void SetOrderBeforeAdd(string groupName, DateTime lineDateTime, Order order)
        {
            order.GroupName = groupName;
            order.CreateTime = DateTime.Now;
            order.ModifyTime = DateTime.Now;
        }

        private bool checkOrderValid(Order order)
        {
            return order.City != null && order.District != null;
        }

        private void SetLineDateTime(DateTime todayDateTime, ref DateTime lineDateTime, string line)
        {
            string hourMinPattern = regexPatterns.DateTimePatterns.HourMinPattern;
            if (Regex.IsMatch(line, hourMinPattern))
            {
                var lineHourMin = dateHelper.GetHourMinFromTxt(line);
                lineDateTime = todayDateTime.Date + lineHourMin.TimeOfDay;
            }
        }

        private DateTime SetDay(DateTime todayDateTime, string line)
        {
            string datePattern = regexPatterns.DateTimePatterns.DatePattern;

            if (Regex.IsMatch(line, datePattern))
            {
                todayDateTime = dateHelper.GetDateFromTxt(line);
            }

            return todayDateTime;
        }
    }
}
