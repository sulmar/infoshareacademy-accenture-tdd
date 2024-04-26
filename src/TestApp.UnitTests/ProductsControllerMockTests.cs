using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Mocking;
using Xunit;

namespace TestApp.UnitTests;

public class ProductsControllerMockTests
{
    [Fact]
    public void Get_FirstCall_ShouldCalledGetDbProductRepository()
    {
        // Arrange
        Mock<IProductRepository> mockDbProductRepository = new Mock<IProductRepository>();
        IProductRepository dbProductRepository = mockDbProductRepository.Object;

        Mock<CacheProductRepository> mockCacheProductRepository = new Mock<CacheProductRepository>();
        CacheProductRepository cacheProductRepository = mockCacheProductRepository.Object;

        ProductsController sut = new ProductsController(dbProductRepository, cacheProductRepository);

        mockDbProductRepository.Setup(db => db.Get(It.IsAny<int>())).Verifiable();

        // Act
        sut.Get(1);

        // Assert
        mockDbProductRepository.Verify(db => db.Get(It.IsAny<int>()), Times.Once);


    }

    [Fact]
    public void Get_SecondCall_ShouldNotCalledGetDbProductRepository()
    {
        // Arrange
        Mock<IProductRepository> mockDbProductRepository = new Mock<IProductRepository>();
        IProductRepository dbProductRepository = mockDbProductRepository.Object;

        ProductsController sut = new ProductsController(dbProductRepository, new CacheProductRepository());

        mockDbProductRepository.Setup(db => db.Get(It.IsAny<int>()))
            .Returns(new Product(1, "a", 10));

        sut.Get(1);

        // Act
        sut.Get(1);

        // Assert
        mockDbProductRepository.Verify(db => db.Get(It.IsAny<int>()), Times.Once);


    }



}
