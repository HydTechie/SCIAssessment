using ProductApi.Models;

namespace ProductApi.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll(string? category, decimal? minPrice, decimal? maxPrice, int page, int pageSize);
        Product? GetById(int id);
        Product Add(Product product);
        bool Update(int id, Product updated);
        bool Delete(int id);
       
    }
}