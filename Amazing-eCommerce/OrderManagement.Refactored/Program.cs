namespace OrderManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            OrderManager orderManager = new OrderManager(database);
            orderManager.DoWork();

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
