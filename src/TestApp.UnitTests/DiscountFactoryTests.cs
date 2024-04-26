using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class DiscountFactoryTests
{
    [Fact]
    public void AddDiscountCodeToPool_EmptyDiscountCode_ShouldThrowsArgumentException()
    {
        // Arrange
        var sut = new DiscountFactory();

        // Act
        Action act = () => sut.AddDiscountCodeToPool(string.Empty);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void AddDiscountCodeToPool_TwiceTheSameDiscountCode_ShouldThrowsInvalidOperationException()
    {
        // Arrange
        var sut = new DiscountFactory();
        sut.AddDiscountCodeToPool("a");

        // Act
        Action act = () => sut.AddDiscountCodeToPool("a");

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void AddDiscountCodeToPool_NotEmptyDiscountCode_DiscountCodePoolHasDiscountCode()
    {
        // Arrange
        var sut = new DiscountFactory();

        // Act
        sut.AddDiscountCodeToPool("a");

        // Assert
        Assert.Collection(sut.DiscountCodePool, item => item.Contains("a"));
    }
}
