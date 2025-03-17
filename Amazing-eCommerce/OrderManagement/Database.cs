using OrderManagement.Model;

namespace OrderManagement
{
    public class Database
    {
        static List<Customer> _customers;
        static List<Order> _orders;
        static List<Product> _products;
        static Database()
        {
            _customers = new List<Customer>
            {
                new Customer { CustomerId = 1, Name = "John Doe", Address = "123 Elm St", State = "CA" },
                new Customer { CustomerId = 2, Name = "Jane Doe", Address = "456 Oak St", State = "NY" }
            };

            int productIdIndex = 1;
            _products = new List<Product>
            {
                new Product { Id = productIdIndex++, Name = "Pixel 9a", Price = 100.20m },
                new Product { Id = productIdIndex++, Name = "iPhone 21", Price = 254.30m },
                new Product { Id = productIdIndex++, Name = "Laptop Super Computer", Price = 789.40m },
                new Product { Id = productIdIndex++, Name = "Type-C cable", Price = 14.30m },
                new Product { Id = productIdIndex++, Name = "Power Bank", Price = 74.30m },
                new Product { Id = productIdIndex++, Name = "Webcam", Price = 44.30m }
            };

            _orders = new List<Order>();
        }

        public static int ProductCount => _products.Count;

        public static Customer GetCustomer(int customerId)
        {
            return _customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        public static Product GetProduct(int productId)
        {
            return _products.FirstOrDefault(p => p.Id == productId);
        }

        public static Order CreateOrder()
        {
            _orders.Clear();
            var order = new Order
            {
                OrderId = 1,
            };

            _orders.Add(order);

            return order;
        }

        public static void ClearOrders(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            _orders.Remove(order);
        }

        public static Order GetOrder(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);

            //if(order == null)
            //    return CreateOrder();

            return order;
        }

        public static void SubmitOrder(int orderId)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            order.Status = OrderStatus.Placed;
        }
    }
}
