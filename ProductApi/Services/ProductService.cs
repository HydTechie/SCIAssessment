using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Repositories;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;    
 
    public ProductService(IProductRepository repository)
    {
        _repository = repository;   
    }
    public IEnumerable<Product> GetAll(string? category, decimal? minPrice, decimal? maxPrice, int page, int pageSize)
    {
        return _repository.GetAll(category, minPrice,   maxPrice, page, pageSize);  
    }

    public Product? GetById(int id) => _repository.GetById(id);
 
    public Product UpSert(Product upserted)
    {
        var product = _repository.GetById(upserted.ProductID);
        if (product != null)
        {
            //existing update 
            _repository.Update(product.ProductID, upserted);
            //updated entity
            return _repository.GetById(product.ProductID);
        }
        else
        {
            // new product 
           return _repository.Add(upserted);
        }
    }

    public bool SoftDelete(int id)
    {
        var product = GetById(id);
        if( product != null )
        {
            product.IsActive = false; // soft delete
            return _repository.Update(id, product);
        }
        return false;    
    }

    
 
    
}
