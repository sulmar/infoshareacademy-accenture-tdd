using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class DiscountFactoryProxyTests
{
    readonly DiscountFactoryProxy sut;

    public DiscountFactoryProxyTests()
    {
        // Arrange
        sut = new DiscountFactoryProxy(new PernamentDiscountFactory());
    }

    [Fact]
    public void AddDiscountCodeToPool_EmptyDiscountCode_ShouldThrowsArgumentException()
    {
        // Act
        Action act = () => sut.AddDiscountCodeToPool(string.Empty);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void AddDiscountCodeToPool_TwiceTheSameDiscountCode_ShouldThrowsInvalidOperationException()
    {
        // Arrange
        sut.AddDiscountCodeToPool("a");

        // Act
        Action act = () => sut.AddDiscountCodeToPool("a");

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void AddDiscountCodeToPool_SingleDiscountCode_DiscountCodePoolHasDiscountCode()
    {
        // Act
        sut.AddDiscountCodeToPool("a");

        // Assert
        Assert.NotEmpty(sut.DiscountCodePool);
        Assert.Collection(sut.DiscountCodePool, item => item.Contains("a"));
    }

    [Fact]
    public void Create_FirstSingleUseDiscountCode_ShouldReturns50PercentageDiscount()
    {
        // Arrange
        sut.AddDiscountCodeToPool("a");

        // Act
        var result = sut.Create("a");

        // Assert
        Assert.Equal(0.5m, result);
    }

    [Fact]
    public void Create_UseDiscountCode_ShouldRemoveDiscountCode()
    {
        // Arrange
        sut.AddDiscountCodeToPool("a");

        // Act
        sut.Create("a");

        // Assert
        Assert.Empty(sut.DiscountCodePool);
    }

    [Fact]
    public void Create_SecondSingleUseDiscountCode_ShouldThrowsArgumentException()
    {
        // Arrange
        sut.AddDiscountCodeToPool("a");
        sut.Create("a");

        // Act
        Action act = () => sut.Create("a");

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Invalid discount code", exception.Message);
    }
}
