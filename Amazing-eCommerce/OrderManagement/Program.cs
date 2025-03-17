namespace OrderManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderManager orderManager = new OrderManager();
            orderManager.DoWork();

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
