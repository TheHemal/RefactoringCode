using Xunit;
using OrderManagement.Model;

namespace OrderManagement.Refactored.UnitTest
{
    public class DatabaseTests
    {
        private Database _database;

        public DatabaseTests()
        {
            _database = new Database();
        }

        [Fact]
        public void GetCustomer_ShouldReturnCorrectCustomer_WhenCustomerExists()
        {
            // Act
            var customer = _database.GetCustomer(1);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(1, customer.CustomerId);
            Assert.Equal("John Doe", customer.Name);
        }

        [Fact]
        public void GetCustomer_ShouldReturnDefaultCustomer_WhenCustomerDoesNotExist()
        {
            // Act
            var customer = _database.GetCustomer(999);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(0, customer.CustomerId); // Default customer
        }

        [Fact]
        public void CreateOrder_ShouldReturnNewOrder()
        {
            // Act
            var order = _database.CreateOrder();

            // Assert
            Assert.NotNull(order);
            Assert.Equal(1, order.OrderId);
        }

        [Fact]
        public void GetProduct_ShouldReturnCorrectProduct_WhenProductExists()
        {
            // Act
            var product = _database.GetProduct(1);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(1, product.Id);
            Assert.Equal("Pixel 9a", product.Name);
        }

        [Fact]
        public void GetProduct_ShouldReturnDefaultProduct_WhenProductDoesNotExist()
        {
            // Act
            var product = _database.GetProduct(999);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(0, product.Id); // Default product
        }

        [Fact]
        public void SubmitOrder_ShouldSetOrderStatusToPlaced_WhenOrderExists()
        {
            // Arrange
            var order = _database.CreateOrder();

            // Act
            _database.SubmitOrder(order.OrderId);

            // Assert
            Assert.Equal(OrderStatus.Placed, order.Status);
        }

        [Fact]
        public void ClearOrders_ShouldRemoveOrderFromDatabase_WhenOrderExists()
        {
            // Arrange
            var order = _database.CreateOrder();

            // Act
            _database.ClearOrders(order.OrderId);

            // Assert
            Assert.Null(_database.GetOrder(order.OrderId));
        }
    }
}