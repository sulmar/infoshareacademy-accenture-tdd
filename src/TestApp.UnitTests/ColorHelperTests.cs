using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class ColorHelperTests
{
    [Fact]
    public void DetermineColor_BelowPercentageZero_ShouldReturnsThrowsArgumentRangeException()
    {
        throw new NotImplementedException();
    }


    [Fact]
    public void DetermineColor_AbovePercentage100_ShouldReturnsThrowsArgumentRangeException()
    {
        throw new NotImplementedException();
    }

    [Theory]
    [InlineData(0, "Green")]
    [InlineData(49, "Green")]
    [InlineData(50, "Yellow")]
    [InlineData(80, "Yellow")]
    [InlineData(81, "Red")]
    public void DetermineColor_Threshold_ShouldReturnsExpectedColor(int value, string expected)
    {
        var result = ColorConverter.ConvertColor(value);

        Assert.Equal(expected, result); 
    }
}
