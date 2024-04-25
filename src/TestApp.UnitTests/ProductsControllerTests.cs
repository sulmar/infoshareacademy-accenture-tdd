using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class ProductsControllerTests
{
    ProductsController sut;

    const int ExistingProductId = 1;
    const int NotExistingProductId = 999;

    public ProductsControllerTests()
    {
        sut = new ProductsController(new CacheProductRepository(new DbProductRepository()));
    }

    [Fact]
    public void Get_FirstCall_ShouldSetCacheHitEqualZero()
    {
        // Arrange

        // Act
        var response = sut.Get(ExistingProductId);

        // Assert
        Assert.Equal(0,  response.CacheHit);
    }

    [Fact]
    public void Get_SecondCall_ShouldSetCacheHitEqualOne() 
    {
        // Arrange
        sut.Get(ExistingProductId);

        // Act
        var response = sut.Get(ExistingProductId);

        // Assert
        Assert.Equal(1, response.CacheHit);
    }


    [Fact]
    public void Get_ExistingProductId_ShouldReturnProduct()
    {
        // Arrange

        // Act
        var result = sut.Get(ExistingProductId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

    }

    [Fact]
    public void Get_NotExistingProductId_ShouldReturnProduct()
    {
        // Arrange

        // Act
        var result = sut.Get(NotExistingProductId);

        // Assert
        Assert.Null(result);       

    }
}
