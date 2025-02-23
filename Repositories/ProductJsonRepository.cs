using System.Text.Json;
using POS_system.Models;
using POS_system.Repositories.Base;

public class ProductJsonRepository : IProductRepository
{
    private readonly string _filePath = "products.json";

    private List<Product> LoadProducts()
    {
        if (!File.Exists(_filePath)) File.WriteAllText(_filePath, "[]");
        return JsonSerializer.Deserialize<List<Product>>(File.ReadAllText(_filePath)) ?? new List<Product>();
    }

    private void SaveProducts(List<Product> products)
    {
        File.WriteAllText(_filePath, JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true }));
    }

    public IEnumerable<Product> GetAll() => LoadProducts();

    public Product? GetProductById(int id) => LoadProducts().FirstOrDefault(p => p.Id == id);

    public void CreateProduct(Product product)
    {
        ValidateProduct(product);
        var products = LoadProducts();
        products.Add(product);
        SaveProducts(products);
    }

    public void UpdateProduct(int id, Product product)
    {
        ValidateProduct(product);
        var products = LoadProducts();
        var index = products.FindIndex(p => p.Id == id);
        if (index == -1) throw new Exception("Product not found.");
        products[index] = product;
        SaveProducts(products);
    }

    public void DeleteProductById(int id)
    {
        var products = LoadProducts();
        products.RemoveAll(p => p.Id == id);
        SaveProducts(products);
    }

    public void ValidateProduct(Product product)
    {
        var validationException = new ValidationException();
        if (string.IsNullOrEmpty(product.Name)) validationException.validationResponses.Add(new ValidationResponse(nameof(product.Name), "Product name can't be empty!"));
        if (string.IsNullOrEmpty(product.Type)) validationException.validationResponses.Add(new ValidationResponse(nameof(product.Type), "Product type can't be empty!"));
        if (product.Price <= 0) validationException.validationResponses.Add(new ValidationResponse(nameof(product.Price), "Price must be greater than zero!"));
        if (validationException.validationResponses.Any()) throw validationException;
    }
}
