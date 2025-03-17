namespace OrderManagement
{
    public class OrderManager
    {
        public OrderManager()
        {
        }

        public void DoWork()
        {
            Console.WriteLine("Hello, Welcome to Order Management! Please place your order and calculate the total price that inlcudes shipping cost.");

            do
            {
                PrintMenu();
                var menuSelection = int.Parse(Console.ReadLine());
                if (menuSelection == 5) break;

                Random number = new Random();
                var rndProductId = number.Next(1, Database.ProductCount);
                PerformMenuSelection(menuSelection, 1, rndProductId);
            } while (true);
        }

        public void PrintMenu()
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Performing selection...");

            Console.WriteLine("1. Create Order");
            Console.WriteLine("2. Add Product");
            Console.WriteLine("3. View Order");
            Console.WriteLine("4. Calculate Order Total");
            Console.WriteLine("5. Place Order");
            Console.WriteLine("6. Cancel Order");
            Console.WriteLine("7. Exit");

            Console.WriteLine("------------------------");
        }

        public void PerformMenuSelection(int selection, int? orderId, int? productId)
        {
            switch (selection)
            {
                case 1:
                    CreateOrder();
                    break;
                case 2:
                    AddProduct(productId.Value); 
                    break;
                case 3:
                    ViewOrder(orderId.Value); 
                    break;
                case 4:
                    CalculateOrderTotal(orderId.Value);
                    break;
                case 5:
                    PlaceOrder(orderId.Value);
                    break;
                case 6:
                    CancelOrder(orderId.Value);
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }
        }

        private void CancelOrder(int orderId)
        {
            Database.ClearOrders(orderId);
        }

        private void PlaceOrder(int orderId)
        {
            Database.SubmitOrder(orderId);
        }

        private void ViewOrder(int orderId)
        {
            var dbOrder = Database.GetOrder(orderId);
            Console.WriteLine($"Order ID: {dbOrder.OrderId}");
            Console.WriteLine($"State: {dbOrder.State}");
            Console.WriteLine("Products:");
            Console.WriteLine("---------");
            int index = 1;
            dbOrder.Products.ForEach(p => Console.WriteLine($"{index++}. {p.Name}: {p.Price}"));

            Console.WriteLine("Product Total Price: " + dbOrder.Products.Sum(p => p.Price));
        }

        private void AddProduct(int productId)
        {
            var dbProduct = Database.GetProduct(productId);
            Console.WriteLine($"Product added: {dbProduct.Name}");

            var dbOrder = Database.GetOrder(1);
            dbOrder.Products.Add(dbProduct);
        }

        private void CalculateOrderTotal(int orderId)
        {
            var totalPrice = CalculateTotalPrice(orderId);

            Console.WriteLine($"Total Price + tax + shipping: {totalPrice}");
        }

        private int CreateOrder()
        {
            Console.WriteLine("Enter Customer Id:");
            int customerId = int.Parse(Console.ReadLine());
            var customer = Database.GetCustomer(customerId);

            var dbOrder = Database.CreateOrder();
            dbOrder.Customer = customer;

            Console.WriteLine($"Order created with ID: {dbOrder.OrderId}");

            return dbOrder.OrderId;
        }

        public decimal CalculateTotalPrice(int orderId)
        {
            //Load the order from database
            var order = Database.GetOrder(orderId);
            var totalPriceOfAllProducts = order.Products.Sum(p => p.Price);

            //Calculate tax, default 12% tax
            decimal taxRate = 0.12m;
            if (order.State == "CA")
                taxRate = 0.07m;
            else if (order.State == "NY")
                taxRate = 0.06m;
            else if (order.State == "CO")
                taxRate = 0.03m;
            else if (order.State == "TX")
                taxRate = 0.00m;

            decimal finalTax = totalPriceOfAllProducts * taxRate;

            //Calculate shipping
            decimal shippingCost = 10;
            if (totalPriceOfAllProducts > 300 && totalPriceOfAllProducts < 700)
            {
                shippingCost = 5;
            }
            else if (totalPriceOfAllProducts > 700)
            {
                shippingCost = 0;
            }

            var totalPrice = totalPriceOfAllProducts + finalTax + shippingCost;

            return totalPrice;
        }
    }
}
