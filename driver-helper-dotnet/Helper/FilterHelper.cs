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
        private readonly DateHelper _dateHelper;
        private readonly RegexPatterns _regexPatterns;
        private readonly MatchHelper _matchHelper;
        private readonly SettingsHelper _settingsHelper;
        private readonly int _scanLimit;

        private bool _isAddressMatched { get; set; } = false;
        private bool _isTimeMatched { get; set; } = false;
        private Match _addressMatch { get; set; } = null;
        private Match _timeMatch { get; set; } = null;
        private Match _dropoffAddressMatch { get; set; } = null;
        private int _currentLine { get; set; } = 0;
        private int _scanCount { get; set; } = 0;

        public FilterHelper()
        {
            this._dateHelper = new DateHelper();
            this._regexPatterns = new RegexPatterns();
            this._matchHelper = new MatchHelper();
            this._settingsHelper = new SettingsHelper();
            this._scanLimit = _settingsHelper.GetOrderSize();
        }
        public List<Order> GetOrdersByFilter(string[] lines, string groupName, CancellationToken cancellationToken)
        {
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

                // Interact with UI
                _currentLine += 1;
                if (_currentLine >= 500)
                {
                    View.FormView.CurrentLine += _currentLine;
                    _currentLine = 0;
                    Thread.Sleep(1);
                }


                SetLineDateTime(ref todayDateTime, ref lineDateTime, line);

                if (!_isAddressMatched)
                    _addressMatch = _matchHelper.RegexMatch(line, _regexPatterns.AddressPatterns);
                if (!_isTimeMatched)
                    _timeMatch = _matchHelper.RegexMatch(line, _regexPatterns.TimePatterns);
                _dropoffAddressMatch = _matchHelper.RegexMatch(line, _regexPatterns.DropoffPatterns);

                // Pickup
                if (_addressMatch.Success && !_isAddressMatched)
                {
                    ProcessAddress(lineDateTime, order);

                    continue;
                }


                // PickupTime
                if (_timeMatch.Success && !_isTimeMatched)
                {
                    _isTimeMatched = true;
                    DateTime pickupTime = _dateHelper.GetHourMinFromTxt(_timeMatch.Groups[1].Value);
                    order.PickUpTime = todayDateTime.Date + pickupTime.TimeOfDay;

                    continue;
                }

                // Dropoff
                if (_dropoffAddressMatch.Success)
                {
                    order = ProcessDropoff(groupName, lineDateTime, orders, order);

                    continue;
                }

                if(!string.IsNullOrEmpty(order.Address))
                    _scanCount++;
                // Could not find the dropoff in range of scan limit
                if (isOverScanLimit(_scanCount))
                {
                    if (string.IsNullOrEmpty(order.Address))
                    {
                        SetOrderTime(lineDateTime, order); // if address doesn not exist, then the orderTime need to be set
                    }
                    order.PickUpDrop = "找不到下車地點";
                    order.IsException = !checkOrderValid(order);
                    SetOrderBeforeAdd(groupName, lineDateTime, order);
                    orders.Add(order);
                    ResetOrder(out order);
                }

                Debug.WriteLine(line);
            }


            return orders;
        }

        private void ProcessAddress(DateTime lineDateTime, Order order)
        {
            _isAddressMatched = true;
            // reset scan count
            _scanCount = 0;
            InitOrder(lineDateTime, order);

            order.Address = _addressMatch.Groups[1].Value.Trim().Replace("：", "");

            // City
            Match cityMatch = _matchHelper.RegexMatch(order.Address, _regexPatterns.CityPatterns);
            if (cityMatch.Success)
            {
                order.City = cityMatch.Groups[1].Value.Trim();
            }

            // District
            Match districtMatch = _matchHelper.RegexMatch(order.Address, _regexPatterns.DistrictPatterns);
            if (districtMatch.Success)
            {
                order.District = districtMatch.Groups[0].Value.Trim();
            }
        }

        private Order ProcessDropoff(string groupName, DateTime lineDateTime, List<Order> orders, Order order)
        {
            // Is pickUpAddress empty
            if (string.IsNullOrWhiteSpace(order.Address))
            {
                order.Address = "此單找不到上車地點";
                SetOrderTime(lineDateTime, order);
            }
            // Dropoff Address 
            order.PickUpDrop = _dropoffAddressMatch.Groups[1].Value.Trim();
            order.IsException = !checkOrderValid(order);

            SetOrderBeforeAdd(groupName, lineDateTime, order);
            orders.Add(order);

            // Reset
            ResetOrder(out order);

            return order;
        }

        private void ResetOrder(out Order order)
        {
            order = new Order();
            _isAddressMatched = false;
            _isTimeMatched = false;
        }

        private bool isOverScanLimit(int scanCount)
        {
            return (scanCount > _scanLimit);
        }

        private static void InitOrder(DateTime lineDateTime, Order order)
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

        private void SetLineDateTime(ref DateTime todayDateTime, ref DateTime lineDateTime, string line)
        {
            string hourMinPattern = _regexPatterns.DateTimePatterns.HourMinPattern;
            SetDate(ref todayDateTime, line);

            if (Regex.IsMatch(line, hourMinPattern))
            {
                var lineHourMin = _dateHelper.GetHourMinFromTxt(line);
                lineDateTime = todayDateTime.Date + lineHourMin.TimeOfDay;
            }
        }

        private void SetDate(ref DateTime todayDateTime, string line)
        {
            string datePattern = _regexPatterns.DateTimePatterns.DatePattern;

            if (Regex.IsMatch(line, datePattern))
            {
                todayDateTime = _dateHelper.GetDateFromTxt(line);
            }
        }
    }
}
