using ADO.NET_Fundamentals.Data;
using ADO.NET_Fundamentals.Models;

string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"; 

DataAccess dataAccess = new(connectionString);

// Создание продукта
Product newProduct = new()
{
    Id = 1,
    Name = "Sample Product",
    Description = "A sample product",
    Weight = 1.5,
    Height = 10,
    Width = 5,
    Length = 20
};
dataAccess.CreateProduct(newProduct);

// Обновление продукта
newProduct.Name = "Updated Product";
dataAccess.UpdateProduct(newProduct);

// Удаление продукта
dataAccess.DeleteProduct(newProduct.Id);

// Создание заказа
Order newOrder = new()
{
    Id = 1,
    Status = "InProgress",
    CreatedDate = DateTime.Now,
    UpdatedDate = DateTime.Now,
    ProductId = 1
};
dataAccess.CreateOrder(newOrder);

// Обновление заказа
newOrder.Status = "Done";
dataAccess.UpdateOrder(newOrder);

// Удаление заказа
dataAccess.DeleteOrder(newOrder.Id);

// Получение списка продуктов
var products = dataAccess.GetAllProducts();

// Получение заказов по фильтру
DateTime startDate = new(2023, 1, 1);
DateTime endDate = new(2023, 12, 31);
string status = "InProgress";
int productId = 1;
var filteredOrders = dataAccess.GetOrdersByFilter(startDate, endDate, status, productId);

// Удаление заказов по фильтру
dataAccess.BulkDeleteOrdersByFilter(startDate, endDate, status, productId);