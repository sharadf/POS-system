namespace POS_system.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using POS_system.Models;
using POS_system.Repositories.Base;


[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository productRepository;
    private readonly string _connectionString;

    public ProductController(IProductRepository productRepository, IConfiguration configuration)
    {
        this.productRepository = productRepository;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        var products = productRepository.GetAll();
        return Ok(products);
    }


    [HttpPost]
    public ActionResult CreateProduct([FromBody]Product product)
    {
        try
        {
            productRepository.CreateProduct(product);
            return Ok();
        }

        catch (ValidationException ex)
        {
            return BadRequest(ex.validationResponses);
        }
    }


    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody]Product product)
    {
        productRepository.UpdateProduct(id, product);
        return NoContent();
    }


    [HttpGet("{id}")]
    public ActionResult<Product> GetProductById(int id)
    {
        var foundProduct = productRepository.GetProductById(id);
        if (foundProduct == null)
            return base.NotFound();
        
        return base.Ok(foundProduct);
    }

    
    [HttpDelete("{id}")]
    public IActionResult DeleteProductById(int id)
    {
        productRepository.DeleteProductById(id);
        return Ok();
    }
}