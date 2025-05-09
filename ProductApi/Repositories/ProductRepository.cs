using ProductApi.Models;

namespace ProductApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();
    private int _nextId = 1;

    public IEnumerable<Product> GetAll(string? category, decimal? minPrice, decimal? maxPrice, int page, int pageSize)
    {
        var query = _products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice.Value);

        return query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public Product? GetById(int id) => _products.FirstOrDefault(p => p.ProductID == id);

    public Product Add(Product product)
    {
        product.ProductID = _nextId++;
        _products.Add(product);
        return product;
    }

    public bool Update(int id, Product updated)
    {
        var existing = GetById(id);
        if (existing == null) return false;

        existing.Name = updated.Name;
        existing.Category = updated.Category;
        existing.Quantity = updated.Quantity;
        existing.Price = updated.Price;
        return true;
    }

    public bool Delete(int id)
    {
        var product = GetById(id);
        return product != null && _products.Remove(product);
    }

    public ProductRepository()
    {
        SeedData();
    }

    private void SeedData()
    {
        var sampleProducts = new List<Product>
    {
        new() { Name = "Laptop", Category = "Electronics", Quantity = 10, Price = 999.99M },
        new() { Name = "Mouse", Category = "Electronics", Quantity = 50, Price = 25.50M },
        new() { Name = "Keyboard", Category = "Electronics", Quantity = 35, Price = 45.99M },
        new() { Name = "Desk", Category = "Furniture", Quantity = 15, Price = 150.00M },
        new() { Name = "Chair", Category = "Furniture", Quantity = 20, Price = 85.00M },
        new() { Name = "Notebook", Category = "Stationery", Quantity = 200, Price = 3.49M },
        new() { Name = "Pen", Category = "Stationery", Quantity = 500, Price = 1.25M },
        new() { Name = "Monitor", Category = "Electronics", Quantity = 18, Price = 175.00M },
        new() { Name = "Printer", Category = "Electronics", Quantity = 12, Price = 249.99M },
        new() { Name = "Bookshelf", Category = "Furniture", Quantity = 5, Price = 120.00M },
        new() { Name = "Sticky Notes", Category = "Stationery", Quantity = 300, Price = 2.99M },
        new() { Name = "Coffee Table", Category = "Furniture", Quantity = 8, Price = 199.99M },
        new() { Name = "Headphones", Category = "Electronics", Quantity = 25, Price = 59.99M },
        new() { Name = "Whiteboard", Category = "Furniture", Quantity = 7, Price = 89.95M },
        new() { Name = "USB Drive", Category = "Electronics", Quantity = 75, Price = 15.00M },
        new() { Name = "File Folder", Category = "Stationery", Quantity = 150, Price = 0.99M },
        new() { Name = "Lamp", Category = "Furniture", Quantity = 10, Price = 34.95M },
        new() { Name = "Tablet", Category = "Electronics", Quantity = 14, Price = 299.99M },
        new() { Name = "Eraser", Category = "Stationery", Quantity = 400, Price = 0.50M },
        new() { Name = "Pen Stand", Category = "Stationery", Quantity = 100, Price = 4.75M },
    };

        foreach (var product in sampleProducts)
        {
            Add(product); // Reuse Add method to set ID properly
        }
    }

}
