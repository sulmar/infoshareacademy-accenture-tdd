using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class PernamentDiscountFactoryTests
{
    [Fact]
    public void Create_DiscountCodeSAVE10NOW_ShouldReturns10PercentageDiscount()
    {
        // Arrange
        var sut = new PernamentDiscountFactory();

        // Act
        var result = sut.Create("SAVE10NOW");

        // Assert
        Assert.Equal(0.1m, result);
    }

    [Fact]
    public void Create_DiscountCodeDISCOUNT20OFF_ShouldReturns20PercentageDiscount()
    {
        // Arrange
        var sut = new PernamentDiscountFactory();

        // Act
        var result = sut.Create("DISCOUNT20OFF");

        // Assert
        Assert.Equal(0.2m, result);
    }

    [Fact]
    public void Create_EmptyDiscountCode_ShouldReturns0PercentageDiscount()
    {
        // Arrange
        var sut = new PernamentDiscountFactory();

        // Act
        var result = sut.Create(string.Empty);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateDiscount_InvalidDiscountCode_ShouldThrowsArgumentException()
    {
        // Arrange
        var sut = new PernamentDiscountFactory();

        // Act
        Action act = () => sut.Create("a");

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Invalid discount code", exception.Message);
    }
}
