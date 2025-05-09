using ProductApi.Models;

namespace ProductApi.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll(string? category, decimal? minPrice, decimal? maxPrice, int page, int pageSize);
        Product? GetById(int id);
        //Product Add(Product product);
        //Product Update(Product product);
        Product UpSert(Product upserted);
        bool SoftDelete(int id);
       
    }
}