using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ADOExample
{
   public class ProductDAO
    {
        public static bool Insert(Product product)
        {
            using (var connection = DBConfig.Connection())
            {
                const string query = "Insert Into Products (Name, Value) Values (@name, @value)";
                var cmd = DBConfig.Command(query, connection);
                cmd.Parameters.Add(DBConfig.Parameter("@name", product.Name));
                cmd.Parameters.Add(DBConfig.Parameter("@value", product.Value));
                var countResult = cmd.ExecuteNonQuery();
                return countResult > 0;
            }
        }

       public static bool Update(Product product)
       {
           using (var connection = DBConfig.Connection())
           {
               const string query = "Update Products Set Name = @name, Value = @value where ID = @id";
               var cmd = DBConfig.Command(query, connection);
               cmd.Parameters.Add(DBConfig.Parameter("@name", product.Name));
               cmd.Parameters.Add(DBConfig.Parameter("@value", product.Value));
               cmd.Parameters.Add(DBConfig.Parameter("@id", product.ID));
               var countResult = cmd.ExecuteNonQuery();
               return countResult > 0;
           }
       }

       public static bool Delete(int id)
       {
           using (var connection = DBConfig.Connection())
           {
               const string query = "Delete from Products where ID = @id";
               var cmd = DBConfig.Command(query, connection);
               cmd.Parameters.Add(DBConfig.Parameter("@id", id));
               var countResult = cmd.ExecuteNonQuery();
               return countResult > 0;
           }
       }

       public static Product SelectById(int id)
       {
           using (var connection = DBConfig.Connection())
           {          
               const string query = "Select * from Products where id = @id";
               var cmd = DBConfig.Command(query, connection);
               cmd.Parameters.Add(DBConfig.Parameter("@id", id));
               using (var reader = cmd.ExecuteReader())
               {
                   var product = new Product();
                   while (reader.Read())
                   {
                       product.ID = (int) reader["ID"];
                       product.Name = reader["Name"].ToString();
                       product.Value = (decimal) reader["Value"];
                   }
                   return product;
               }
           }
       }

       public static List<Product> SelectAll()
       {
           using (var connection = DBConfig.Connection())
           {
               const string query = "Select * from Products";
               var cmd = DBConfig.Command(query, connection);
               using (var reader = cmd.ExecuteReader())
               {
                   var products = new List<Product>();
                   while (reader.Read())
                   {
                       var product = new Product
                       {
                           ID = (int) reader["ID"],
                           Name = reader["Name"].ToString(),
                           Value = (decimal) reader["Value"]
                       };
                       products.Add(product);
                   }
                   return products;
               }
           }
       }

       public static List<Product> SelectByOrder(int orderId)
       {
           using (var connection = DBConfig.Connection())
           {
               const string query = "Select * from Products p inner join ProductsOrders po on p.ID = po.IDProduct where po.IDOrder = @orderId";
               var cmd = DBConfig.Command(query, connection);
               cmd.Parameters.Add(DBConfig.Parameter("@orderID", orderId));
               using (var reader = cmd.ExecuteReader())
               {
                   var products = new List<Product>();
                   while (reader.Read())
                   {
                       var product = new Product
                       {
                           ID = (int)reader["ID"],
                           Name = reader["Name"].ToString(),
                           Value = (decimal)reader["Value"]
                       };
                       products.Add(product);
                   }
                   return products;
               }
           }
       }
    }
}
