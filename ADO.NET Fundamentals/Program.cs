using ADO.NET_Fundamentals.Data;
using ADO.NET_Fundamentals.Models;

string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False";


 ProductRepository productRepository = new(connectionString);
 OrderRepository orderRepository = new(connectionString);

// Создание продукта
Product newProduct = new()
{
    Name = "Sample Product",
    Description = "A sample product",
    Weight = 1.5,
    Height = 10,
    Width = 5,
    Length = 20
};
int productId = productRepository.CreateProduct(newProduct);

// Обновление продукта
newProduct.Name = "Updated Product";
productRepository.UpdateProduct(productId,newProduct);

// Создание заказа
Order newOrder = new()
{
    Status = OrderStatus.InProgress,
    CreatedDate = DateTime.Now,
    UpdatedDate = DateTime.Now,
    ProductId = productId,
};
int orderId = orderRepository.CreateOrder(newOrder);

// Обновление заказа
newOrder.Status = OrderStatus.Done;
orderRepository.UpdateOrder(orderId,newOrder);


// Получение заказов по фильтру
DateTime startDate = new DateTime(2023, 1, 1);
DateTime endDate = new DateTime(2023, 12, 31);
OrderStatus status = OrderStatus.InProgress;
var filteredOrders = orderRepository.GetOrdersByFilter(startDate, endDate, status, orderId);

// Удаление заказов по фильтру
orderRepository.BulkDeleteOrdersByFilter(startDate, endDate, status, orderId);

// Удаление заказа
orderRepository.DeleteOrder(orderId);

// Удаление продукта
productRepository.DeleteProduct(newProduct.Id);