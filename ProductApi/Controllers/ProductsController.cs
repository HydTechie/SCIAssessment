using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repositories;
using ProductApi.Services;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    // GET with pagination and filtering
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll(
        string? category, decimal? minPrice, decimal? maxPrice,
        int page = 1, int pageSize = 10)
    {
        if (page <= 0 || pageSize <= 0)
            return BadRequest("Page and PageSize must be greater than 0.");

        var products = _service.GetAll(category, minPrice, maxPrice, page, pageSize);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _service.GetById(id);
        return product == null ? NotFound() : Ok(product);
    }

    //[HttpPost]
    //public ActionResult<Product> Create(Product product)
    //{
    //    if (string.IsNullOrWhiteSpace(product.Name) || product.Price < 0 || product.Quantity < 0)
    //        return BadRequest("Invalid product data.");

    //    var created = _repo.Add(product);
    //    return CreatedAtAction(nameof(GetById), new { id = created.ProductID }, created);
    //}

    // Combinations of Repository methods expose as Upsert from Service!
    [HttpPost("{id}")]
    public ActionResult<Product> Upsert(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name) || product.Price < 0 || product.Quantity < 0)
            return BadRequest("Invalid product data.");

        return _service.UpSert(product) == null ? NotFound() : Ok(product);
    }

    [HttpDelete("{id}")]
    public IActionResult SoftDelete(int id)
    {
        return _service.SoftDelete(id) ? NoContent() : NotFound();
    }
}
