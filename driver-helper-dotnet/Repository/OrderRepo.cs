using Dapper;
using driver_helper_dotnet.Helper;
using driver_helper_dotnet.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace driver_helper_dotnet.Repository
{
    internal class OrderRepo
    {
        private string _connStr;
        public OrderRepo()
        {
            this._connStr = new SettingsHelper().GetConnectionString();
        }
        public void SaveToDB(List<Order> orders)
        {
            var sql = $@" INSERT INTO orders(
                    city, district, address, order_time, pick_up_drop, pick_up_time
                    , weekday, group_name, amount, distance, is_exception
                    , repeat_count, create_time, modify_time)

                      VALUES(@City, @District, @Address, @OrderTime, @PickUpDrop, @PickUpTime
                    , @Weekday, @GroupName, @Amount, @Distance, @IsException
                    , @RepeatCount, @CreateTime, @ModifyTime)";
            try
            {
                using (var conn = new NpgsqlConnection(_connStr))
                {
                    conn.Open();

                    conn.Execute(sql, orders);
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void DeleteExpired(int expiredRangeMonth)
        {
            DateTime deleteTime = DateTime.Now.AddMonths(-expiredRangeMonth);

            var sql = $@" DELETE FROM orders WHERE order_time < @order_time ";
            try
            {
                using(var conn = new NpgsqlConnection(_connStr))
                {
                    conn.Open();
                    conn.Execute(sql, param: new { order_time = deleteTime});
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        
 
    }
}
