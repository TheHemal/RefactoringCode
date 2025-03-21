using OrderManagement.Model;

namespace OrderManagement
{
    public class Database
    {
        private readonly List<Customer> _customers;
        private readonly List<Order> _orders;
        private readonly List<Product> _products;

        public Database()
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

        public int ProductCount => _products.Count;

        public Customer GetCustomer(int customerId)
        {
            try
            {
                return _customers.FirstOrDefault(c => c.CustomerId == customerId) ?? new Customer { CustomerId = 0 };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new Customer { CustomerId = 0 };
            }
        }

        public Product GetProduct(int productId)
        {
            try
            {
                return _products.FirstOrDefault(p => p.Id == productId) ?? new Product { Id = 0 };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new Product { Id = 0 };
            }
        }

        public Order CreateOrder()
        {
            var order = new Order
            {
                OrderId = _orders.Count + 1,
            };

            _orders.Add(order);

            return order;
        }

        public void ClearOrders(int orderId)
        {
            try
            {
                var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    _orders.Remove(order);
                }
                else
                {
                    throw new Exception($"Order with ID {orderId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public Order? GetOrder(int orderId)
        {
            try
            {
                return _orders.FirstOrDefault(o => o.OrderId == orderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public void SubmitOrder(int orderId)
        {
            try
            {
                var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    order.Status = OrderStatus.Placed;
                }
                else
                {
                    throw new Exception($"Order with ID {orderId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
