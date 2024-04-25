using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests
{
    // dotnet add package xunit
    // dotnet add package xunit.runner.visualstudio 

    public class MarkdownFormatterTests
    {
        // Method_Scenario_ExpectedBehavior

        [Theory]
        [InlineData("a")]
        [InlineData("abc")]
        [InlineData("Lorem ipsum")]
        public void FormatAsBold_NotEmptyContent_ReturnsContentEncloseDoubleAsterix(string content)
        {
            // Arrange
            MarkdownFormatter formatter = new MarkdownFormatter();

            // Act
            var result = formatter.FormatAsBold(content);

            // Assert

            // Wariant 1: Test szczegółowy
            // Assert.Equal(expected, result); 

            // Wariant 2: Test ogólny
            Assert.StartsWith("**", result);
            Assert.Contains(content, result);
            Assert.EndsWith("**", result);
        }


        [Fact]
        public void FormatAsBold_EmptyContent_ShouldThrowArgumentNullException()
        {
            // Arrange
            MarkdownFormatter formatter = new MarkdownFormatter();

            // Act
            var act = () => formatter.FormatAsBold(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

     
    }
}
