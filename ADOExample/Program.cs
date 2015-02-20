using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOExample
{
    class Program
    {
        static void Main(string[] args)
        {
            SelectOptionGeneral(OptionsGeneral());
            Console.ReadKey();
        }

        public static void SelectOptionGeneral(string option)
        {
            switch (option)
            {
                case "1":
                    SelectOptionProducts(OptionsForProducts());
                    break;
                case "2":
                    SelectOptionOrders(OptionsForOrders());
                    break;
                case "3":
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Invalid Option!");
                    SelectOptionGeneral(OptionsGeneral());
                    break;
            }
        }

        public static string OptionsGeneral()
        {
            Console.WriteLine("Choose an option");
            Console.WriteLine("1 - Products, 2 - Orders, 3- Exit");
            return Console.ReadLine();

        }

        public static string OptionsForProducts()
        {
            Console.WriteLine("Choose an option for Products");
            Console.WriteLine("1- Add Product, 2- List Products, 3- Update Product, 4- Delete Product, 5- Back");
            return Console.ReadLine();

        }

        public static string OptionsForOrders()
        {
            Console.WriteLine("Choose an option for Orders");
            Console.WriteLine("1- Add Order, 2- List Orders, 3- Delete Order, 4- Back");
            return Console.ReadLine();
        }

        public static void SelectOptionProducts(string option)
        {
            switch (option)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    ListProducts();
                    break;
                case "3":
                    UpdateProduct();
                    break;
                case "4":
                    DeleteProduct();
                    break;
                case "5":
                    SelectOptionGeneral(OptionsGeneral());
                    return;
                default:
                    Console.WriteLine("Invalid Option!");
                    break;
            }
            SelectOptionProducts(OptionsForProducts());
        }

        public static void SelectOptionOrders(string option)
        {
            switch (option)
            {
                case "1":
                    AddOrder();
                    break;
                case "2":
                    ListOrders();
                    break;
                case "3":
                    DeleteOrder();
                    break;
                case "4":
                    SelectOptionGeneral(OptionsGeneral());
                    break;
                default:
                    Console.WriteLine("Invalid Option!");
                    break;
            }
            SelectOptionOrders(OptionsForOrders());
        }

        public static void AddProduct()
        {
            var product = new Product();
            Console.WriteLine("Type the Name: ");
            product.Name = Console.ReadLine();
            Console.WriteLine("Type the Value: ");
            try
            {
                product.Value = Convert.ToDecimal(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value!");
                AddProduct();
            }
            if (!ProductDAO.Insert(product)) return;
            Console.WriteLine("Product {0} added successfully", product.Name);
            Console.WriteLine();
        }

        public static void ListProducts()
        {
            var products = ProductDAO.SelectAll();
            Console.WriteLine("ID ---- NAME ---- VALUE");
            foreach (var product in products)
            {
                Console.WriteLine("{0} -------- {1} ---- {2}", product.ID, product.Name, product.Value);
            }
            Console.WriteLine();
        }

        public static void UpdateProduct()
        {
            ListProducts();
            Console.WriteLine("Type Product ID:");
            try
            {
                var id = Convert.ToInt32(Console.ReadLine());
                var product = ProductDAO.SelectById(id);
                Console.Write("New Name: ");
                product.Name = Console.ReadLine();
                Console.Write("New Value: ");
                product.Value = Convert.ToDecimal(Console.ReadLine());
                ProductDAO.Update(product);
                Console.WriteLine("Product {0} updated successfully", product.Name);
                Console.WriteLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Option!");
                UpdateProduct();
            }

        }

        public static void DeleteProduct()
        {
            ListProducts();
            Console.WriteLine("Type Product ID:");
            try
            {
                var id = Convert.ToInt32(Console.ReadLine());
                ProductDAO.Delete(id);
                Console.WriteLine("Product deleted successfully");
                Console.WriteLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Option!");
                DeleteProduct();
            }
        }

        public static void AddOrder()
        {
            Console.WriteLine("Add Products Type 0 to Exit:");
            ListProducts();
            try
            {
                var order = new Order {Products = new List<Product>()};
                var idProduct = Convert.ToInt32(Console.ReadLine());
                while (idProduct != 0)
                {
                    var product = ProductDAO.SelectById(idProduct); 
                    order.Products.Add(product);
                    order.Total = order.Total + product.Value;
                    Console.WriteLine("Product {0} added to order",product.Name);
                    Console.WriteLine("Add Products Type 0 to Exit:");
                    idProduct = Convert.ToInt32(Console.ReadLine());
                }                
                if (!OrderDAO.Insert(order)) return;
                Console.WriteLine("Order {0} added successfully", order.ID);
                Console.WriteLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid value!");
                AddOrder();
            }

        }

        public static void ListOrders()
        {
            var orders = OrderDAO.SelectAll();
            Console.WriteLine("ID ---- TOTAL");
            foreach (var order in orders)
            {
                Console.WriteLine("{0} ---- {1}", order.ID, order.Total);
                Console.WriteLine("Products:");
                foreach (var product in order.Products)
                {
                    Console.WriteLine("{0} -------- {1} ---- {2}", product.ID, product.Name, product.Value);
                }
            }
            Console.WriteLine();
        }

        public static void DeleteOrder()
        {
            ListOrders();
            Console.WriteLine("Type Order ID:");
            try
            {
                var id = Convert.ToInt32(Console.ReadLine());
                OrderDAO.Delete(id);
                Console.WriteLine("Order deleted successfully");
                Console.WriteLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Option!");
                DeleteOrder();
            }
        }
    }
}
