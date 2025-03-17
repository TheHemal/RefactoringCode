namespace OrderManagement.Model
{
    public class Order
    {
        public Order()
        {
            Products = new List<Product>();
            Status = OrderStatus.New;
        }
        public int OrderId { get; set; }
        public string State
        {
            get
            {
                return Customer.State;
            }
        }

        public List<Product> Products { get; set; }
        public Customer Customer { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        New,
        Placed,
        Shipped,
        Delivered
    }
}
