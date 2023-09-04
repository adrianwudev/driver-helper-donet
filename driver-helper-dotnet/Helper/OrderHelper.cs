using driver_helper_dotnet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace driver_helper_dotnet.Helper
{
    public class OrderHelper
    {
        public List<Order> OrdersGroupByAddress(List<Order> orderList)
        {
            // For each group, keep the earliest order within each hour
            var groupedOrders = new List<Order>();

            foreach (var order in orderList)
            {
                // Check if there is an existing order in the grouped list with the same address and within the same hour
                var existingOrder = groupedOrders.FirstOrDefault(
                    o => o.Address == order.Address && o.OrderTime.Hour == order.OrderTime.Hour);

                if (existingOrder == null)
                {
                    // If there is no existing order with the same address and hour, add the current order to the grouped list
                    groupedOrders.Add(order);
                }
                else
                {
                    // If there is an existing order with the same address and hour, compare their times and keep the earlier one
                    if (order.OrderTime < existingOrder.OrderTime)
                    {
                        groupedOrders.Remove(existingOrder);
                        groupedOrders.Add(order);
                    }
                }
            }

            return groupedOrders;
        }
    }
}
