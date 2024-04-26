using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class PernamentDiscountFactoryTests
{
    readonly PernamentDiscountFactory sut;

    public PernamentDiscountFactoryTests()
    {
        sut = new PernamentDiscountFactory();
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData("SAVE10NOW", 0.1)]
    [InlineData("DISCOUNT20OFF", 0.2)]
    public void Create_ValidDiscountCode_ShouldReturnsPercentage(string discountCode, decimal expected)
    {
        // Act
        var result = sut.Create(discountCode);

        // Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void CalculateDiscount_InvalidDiscountCode_ShouldThrowsArgumentException()
    {
        // Act
        Action act = () => sut.Create("a");

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Invalid discount code", exception.Message);
    }
}
