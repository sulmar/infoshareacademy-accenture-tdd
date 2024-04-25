using System.Collections.Generic;
using System.Linq;
using TestApp.Mocking;

namespace TestApp.Fundamentals;

public class ProductsController
{
    private readonly IProductRepository productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public Product Get(int id)
    {
        Product product = productRepository.Get(id);

        return product;
    }
}


public interface IEntityRepository<T>
{
    T Get(int id);
    void Add(T item);
}

public interface IProductRepository : IEntityRepository<Product>
{
}

public class DbProductRepository : IProductRepository
{
    private readonly IDictionary<int, Product> products;

    public DbProductRepository()
    {
        List<Product> _products =
        [
            new Product(1, "Product 1", 10),
            new Product(2, "Product 2", 20),
            new Product(3, "Product 3", 30)
        ];

        products = _products.ToDictionary(p => p.Id);
    }

    public void Add(Product product)
    {
        products.Add(product.Id, product);
    }

    public Product Get(int id)
    {
        if (products.TryGetValue(id, out Product product))
        {
            return product;
        }
        else
            return null;
    }
}

// Proxy Design Pattern
public class CacheProductRepository : IProductRepository
{
    private readonly IDictionary<int, Product> products;
    private readonly IProductRepository productRepository;

    public CacheProductRepository(IProductRepository productRepository)
    {
        products = new Dictionary<int, Product>();
        this.productRepository = productRepository;
    }

    public void Add(Product product)
    {
        products.Add(product.Id, product);
    }

    public Product Get(int id)
    {
        if (products.TryGetValue(id, out Product product))
        {
            product.CacheHit++;

            return product;
        }
        else
        {
            // Real Subject
            product = productRepository.Get(id);

            if (product != null)
            {
                Add(product);
            }

            return product;
        }
    }

}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public int CacheHit { get; set; }
}
