using ADO.NET_Fundamentals.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ADO.NET_Fundamentals.Data
{
    public class DataAccess
    {
        private readonly string _connectionString;

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        // CRUD operations for Product
        public void CreateProduct(Product product)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            string insertProductQuery = @"
                    INSERT INTO Products (Id, Name, Description, Weight, Height, Width, Length)
                    VALUES (@Id, @Name, @Description, @Weight, @Height, @Width, @Length)
                ";

            using SqlCommand command = new(insertProductQuery, connection);
            command.Parameters.AddWithValue("@Id", product.Id);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Weight", product.Weight);
            command.Parameters.AddWithValue("@Height", product.Height);
            command.Parameters.AddWithValue("@Width", product.Width);
            command.Parameters.AddWithValue("@Length", product.Length);

            command.ExecuteNonQuery();
        }

        public void UpdateProduct(Product product)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            string updateProductQuery = @"
                    UPDATE Products
                    SET Name = @Name, Description = @Description, Weight = @Weight, Height = @Height, Width = @Width, Length = @Length
                    WHERE Id = @Id
                ";

            using SqlCommand command = new(updateProductQuery, connection);
            command.Parameters.AddWithValue("@Id", product.Id);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Weight", product.Weight);
            command.Parameters.AddWithValue("@Height", product.Height);
            command.Parameters.AddWithValue("@Width", product.Width);
            command.Parameters.AddWithValue("@Length", product.Length);

            command.ExecuteNonQuery();
        }

        public void DeleteProduct(int productId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            string deleteProductQuery = @"
                    DELETE FROM Products
                    WHERE Id = @Id
                ";

            using SqlCommand command = new(deleteProductQuery, connection);
            command.Parameters.AddWithValue("@Id", productId);
            command.ExecuteNonQuery();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new();

            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();

                string getAllProductsQuery = @"
                    SELECT Id, Name, Description, Weight, Height, Width, Length
                    FROM Products
                ";

                using SqlCommand command = new(getAllProductsQuery, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Weight = Convert.ToDouble(reader["Weight"]),
                        Height = Convert.ToDouble(reader["Height"]),
                        Width = Convert.ToDouble(reader["Width"]),
                        Length = Convert.ToDouble(reader["Length"])
                    };
                    products.Add(product);
                }
            }

            return products;
        }

        // CRUD operations for Order
        public void CreateOrder(Order order)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            string insertOrderQuery = @"
                    INSERT INTO Orders (Id, Status, CreatedDate, UpdatedDate, ProductId)
                    VALUES (@Id, @Status, @CreatedDate, @UpdatedDate, @ProductId)
                ";

            using SqlCommand command = new(insertOrderQuery, connection);
            command.Parameters.AddWithValue("@Id", order.Id);
            command.Parameters.AddWithValue("@Status", order.Status);
            command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
            command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
            command.Parameters.AddWithValue("@ProductId", order.ProductId);

            command.ExecuteNonQuery();
        }

        public void UpdateOrder(Order order)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            string updateOrderQuery = @"
                    UPDATE Orders
                    SET Status = @Status, CreatedDate = @CreatedDate, UpdatedDate = @UpdatedDate, ProductId = @ProductId
                    WHERE Id = @Id
                ";

            using SqlCommand command = new(updateOrderQuery, connection);
            command.Parameters.AddWithValue("@Id", order.Id);
            command.Parameters.AddWithValue("@Status", order.Status);
            command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
            command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
            command.Parameters.AddWithValue("@ProductId", order.ProductId);

            command.ExecuteNonQuery();
        }

        public void DeleteOrder(int orderId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            string deleteOrderQuery = @"
                    DELETE FROM Orders
                    WHERE Id = @Id
                ";

            using SqlCommand command = new(deleteOrderQuery, connection);
            command.Parameters.AddWithValue("@Id", orderId);
            command.ExecuteNonQuery();
        }

        public List<Order> GetOrdersByFilter(DateTime startDate, DateTime endDate, string status, int productId)
        {
            List<Order> orders = new();

            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();

                string getOrdersByFilterQuery = @"
                    SELECT Id, Status, CreatedDate, UpdatedDate, ProductId
                    FROM Orders
                    WHERE CreatedDate >= @StartDate AND CreatedDate <= @EndDate AND Status = @Status AND ProductId = @ProductId
                ";

                using SqlCommand command = new(getOrdersByFilterQuery, connection);
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@ProductId", productId);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Status = reader["Status"].ToString(),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                        UpdatedDate = Convert.ToDateTime(reader["UpdatedDate"]),
                        ProductId = Convert.ToInt32(reader["ProductId"])
                    };
                    orders.Add(order);
                }
            }

            return orders;
        }

        public void BulkDeleteOrdersByFilter(DateTime startDate, DateTime endDate, string status, int productId)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            string bulkDeleteOrdersQuery = @"
                    DELETE FROM Orders
                    WHERE CreatedDate >= @StartDate AND CreatedDate <= @EndDate AND Status = @Status AND ProductId = @ProductId
                ";

            using SqlCommand command = new(bulkDeleteOrdersQuery, connection);
            command.Parameters.AddWithValue("@StartDate", startDate);
            command.Parameters.AddWithValue("@EndDate", endDate);
            command.Parameters.AddWithValue("@Status", status);
            command.Parameters.AddWithValue("@ProductId", productId);

            command.ExecuteNonQuery();
        }
    }
}
