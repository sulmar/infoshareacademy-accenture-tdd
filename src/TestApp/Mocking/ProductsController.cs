using System.Collections.Generic;

namespace TestApp.Mocking;

public class ProductsController
{
    private readonly IProductRepository productRepository;
    private readonly CacheProductRepository cacheProductRepository;

    public ProductsController(IProductRepository productRepository, CacheProductRepository cacheProductRepository)
    {
        this.productRepository = productRepository;
        this.cacheProductRepository = cacheProductRepository;
    }

    public Product Get(int id)
    {
        Product product = cacheProductRepository.Get(id);

        if (product == null)
        {
            product = productRepository.Get(id);

            if (product != null)
            {
                cacheProductRepository.Add(product);
            }
        }

        return product;
    }
}

public interface IProductRepository
{
    Product Get(int id);
}

public class DbProductRepository : IProductRepository
{
    private readonly IDictionary<int, Product> products;

    public DbProductRepository()
    {
        var products = new Dictionary<int, Product>
        {
            [1] = new Product(1, "Product 1", 10),
            [2] = new Product(2, "Product 2", 20),
            [3] = new Product(3, "Product 3", 30)
        };
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

public class CacheProductRepository : IProductRepository
{
    private readonly IDictionary<int, Product> products;

    public CacheProductRepository()
    {
        products = new Dictionary<int, Product>();
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
}
