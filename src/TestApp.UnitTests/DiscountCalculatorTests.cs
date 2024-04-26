using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class DiscountCalculatorTests
{

    [Fact]
    public void CalculateDiscount_PriceIsNegative_ShouldThrowsArgumentException()
    {
        // Arrange
        var sut = new DiscountCalculator(new PernamentDiscountFactory());

        // Act
        Action act = () => sut.CalculateDiscount(-1, string.Empty);

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Negatives not allowed", exception.Message);
    }


    [Fact]
    public void CalculateDiscount_FirstSingleUseDiscountCode_ShouldReturns50PercentageDiscount()
    {
        // Arrange
        var factory = new DiscountFactoryProxy(new PernamentDiscountFactory());
        factory.AddDiscountCodeToPool("a");

        var sut = new DiscountCalculator(factory);
        

        // Act
        var result = sut.CalculateDiscount(100, "a");

        // Assert
        Assert.Equal(50, result);
    }

    [Fact]
    public void CalculateDiscount_SecondSingleUseDiscountCode_ShouldThrowsArgumentException()
    {
        // Arrange
        var factory = new DiscountFactoryProxy(new PernamentDiscountFactory());
        factory.AddDiscountCodeToPool("a");
        var sut = new DiscountCalculator(factory);
        
        sut.CalculateDiscount(100, "a");

        // Act
        Action act = () => sut.CalculateDiscount(100, "a");

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Invalid discount code", exception.Message);
    }
   
}
