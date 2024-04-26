using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;


public class DiscountCalculatorTests
{
    readonly DiscountCalculator sut;

    public DiscountCalculatorTests()
    {
        sut = new DiscountCalculator(new PernamentDiscountFactory());
    }

    [Fact]
    public void CalculateDiscount_PriceIsNegative_ShouldThrowsArgumentException()
    {
        // Act
        Action act = () => sut.CalculateDiscount(-1, string.Empty);

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Negatives not allowed", exception.Message);
    }

    [Fact]
    public void CalculateDiscount_PriceIsPositive_ShouldReturnsPercentageDiscount()
    {
        // Act
        var result = sut.CalculateDiscount(100, "SAVE10NOW");

        // Assert        
        Assert.Equal(90, result);
    }


}
