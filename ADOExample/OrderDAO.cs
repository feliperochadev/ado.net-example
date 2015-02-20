using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOExample
{
    public class OrderDAO
    {
        public static bool Insert(Order order)
        {
            using (var connection = DBConfig.Connection())
            {
                const string query = "Insert Into Orders (Total) Values (@total)";
                var cmd = DBConfig.Command(query, connection);
                cmd.Parameters.Add(DBConfig.Parameter("@total", order.Total));
                cmd.ExecuteNonQuery();
                const string query2 = "SELECT TOP 1 * FROM Orders ORDER BY ID DESC";
                var cmd2 = DBConfig.Command(query2, connection);
                using (var reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        order.ID = (int)reader["ID"];
                    }
                }
                foreach (var product in order.Products)
                {
                    const string query3 = "Insert Into ProductsOrders (IDProduct, IDOrder) Values (@idProduct, @idOrder)";
                    var cmd3 = DBConfig.Command(query3, connection);
                    cmd3.Parameters.Add(DBConfig.Parameter("@idProduct", product.ID));
                    cmd3.Parameters.Add(DBConfig.Parameter("@idOrder", order.ID));
                    cmd3.ExecuteNonQuery();
                }
                return true;
            }
        }

        public static bool Delete(int id)
        {
            using (var connection = DBConfig.Connection())
            {
                const string query = "Delete from Orders where ID = @id";
                var cmd = DBConfig.Command(query, connection);
                cmd.Parameters.Add(DBConfig.Parameter("@id", id));
                var countResult = cmd.ExecuteNonQuery();
                const string query2 = "Delete from ProductsOrders where IDOrder = @id";
                var cmd2 = DBConfig.Command(query2, connection);
                cmd2.Parameters.Add(DBConfig.Parameter("@id", id));
                var countResult2 = cmd2.ExecuteNonQuery();
                return countResult > 0 && countResult2 > 0;
            }
        }

        public static Order SelectById(int id)
        {
            var order = new Order();
            using (var connection = DBConfig.Connection())
            {
                const string query = "Select * from Orders where id = @id";
                var cmd = DBConfig.Command(query, connection);
                cmd.Parameters.Add(DBConfig.Parameter("@id", id));              
                using (var reader = cmd.ExecuteReader())
                {                  
                    while (reader.Read())
                    {
                        order.ID = (int)reader["ID"];
                        order.Total = (decimal) reader["Name"];
                    }
                }
            }
            order.Products = ProductDAO.SelectByOrder(order.ID);
            return order;
        }

        public static List<Order> SelectAll()
        {
            var orders = new List<Order>();
            using (var connection = DBConfig.Connection())
            {
                const string query = "Select * from Orders";
                var cmd = DBConfig.Command(query, connection);
                using (var reader = cmd.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        var order = new Order()
                        {
                            ID = (int)reader["ID"],
                            Total = (decimal)reader["Total"]
                        };
                        orders.Add(order);
                    }
                }
            }
            foreach (var order in orders)
            {
                order.Products = new List<Product>();
                order.Products = ProductDAO.SelectByOrder(order.ID);
            }
            return orders;
        }
    }
}
