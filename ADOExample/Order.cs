using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOExample
{
    public class Order
    {
        public int ID { get; set; }
        public decimal Total { get; set; }
        public List<Product> Products { get; set; }
    }
}
