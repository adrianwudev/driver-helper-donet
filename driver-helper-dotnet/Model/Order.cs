using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace driver_helper_dotnet.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public DateTime OrderTime { get; set; }
        public string PickUpDrop { get; set; }
        public DateTime PickUpTime { get; set; }
        public string Weekday { get; set; }
        public string GroupName { get; set; }
        public double Amount { get; set; }
        public double Distance { get; set; }
        public bool IsException { get; set; }
        public int RepeatCount { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}
