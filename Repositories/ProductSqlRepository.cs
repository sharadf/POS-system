using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using POS_system.Models;
using POS_system.Repositories.Base;

public class ProductSqlRepository : IProductRepository
{
    private readonly string _connectionString;

    public ProductSqlRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    public IEnumerable<Product> GetAll()
    {
        Console.WriteLine("Executing SQL: SELECT * FROM Products;");
        using var connection = CreateConnection();
        return connection.Query<Product>("SELECT * FROM Products;");
    }


    public Product? GetProductById(int id)
    {
        using var connection = CreateConnection();
        return connection.QueryFirstOrDefault<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
    }

    public void CreateProduct(Product product)
    {
        ValidateProduct(product);
        using var connection = CreateConnection();
        string sql = "INSERT INTO Products (Name, Type, Price) VALUES (@Name, @Type, @Price)";
        connection.Execute(sql, product);
    }

    public void UpdateProduct(int id, Product product)
    {
        ValidateProduct(product);
        using var connection = CreateConnection();
        string sql = "UPDATE Products SET Name = @Name, Type = @Type, Price = @Price WHERE Id = @Id";
        connection.Execute(sql, new { product.Name, product.Type, product.Price, Id = id });
    }

    public void DeleteProductById(int id)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM Products WHERE Id = @Id", new { Id = id });
    }

    public void ValidateProduct(Product product)
    {
        var validationException = new ValidationException();

        if (string.IsNullOrEmpty(product.Name))
        {
            validationException.validationResponses.Add(new ValidationResponse(
                nameof(product.Name),
                "Product name can't be empty!"
            ));
        }

        if (string.IsNullOrEmpty(product.Type))
        {
            validationException.validationResponses.Add(new ValidationResponse(
                nameof(product.Type),
                "Product type can't be empty!"
            ));
        }

        if (product.Price <= 0)
        {
            validationException.validationResponses.Add(new ValidationResponse(
                nameof(product.Price),
                "Price must be greater than zero!"
            ));
        }

        if (validationException.validationResponses.Any())
        {
            throw validationException;
        }
    }
}
