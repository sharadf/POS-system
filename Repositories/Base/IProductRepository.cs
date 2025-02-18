using POS_system.Models;

namespace POS_system.Repositories.Base;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();    
    Product? GetProductById(int id);
    void CreateProduct(Product product);
    void UpdateProduct(int id, Product product);
    void DeleteProductById(int id);
    void ValidateProduct(Product product);
}
