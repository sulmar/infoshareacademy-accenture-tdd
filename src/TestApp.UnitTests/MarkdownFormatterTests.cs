using FluentAssertions;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests
{
    // dotnet add package xunit
    // dotnet add package xunit.runner.visualstudio 

    // dotnet add package FluentAssertions

    public class MarkdownFormatterTests
    {
        const string DoubleAsterix = "**";

        const string ContentEncloseDoubleAsterix = @"^\*{2}.*\*{2}$";

        MarkdownFormatter formatter;

        public MarkdownFormatterTests()
        {
            formatter = new MarkdownFormatter();
        }

        // Method_Scenario_ExpectedBehavior

        [Theory]
        [InlineData("a")]
        [InlineData("abc")]
        [InlineData("Lorem ipsum")]
        public void FormatAsBold_NotEmptyContent_ReturnsContentEncloseDoubleAsterix(string content)
        {
            // Arrange

            // Act
            var result = formatter.FormatAsBold(content);

            // Assert

            // Wariant 1: Test szczegółowy
            // Assert.Equal(expected, result); 

            // Wariant 2: Test ogólny
            Assert.StartsWith(DoubleAsterix, result);
            Assert.Contains(content, result);
            Assert.EndsWith(DoubleAsterix, result);

            // Zastosowanie wyrażenia regularnego
            Assert.Matches(ContentEncloseDoubleAsterix, result);

            // Zastosowanie FluentAssertions
            result.Should()
                .StartWith(DoubleAsterix)
                .And.Contain(content)
                .And.EndWith(DoubleAsterix);
            
            result.Should().MatchRegex(ContentEncloseDoubleAsterix);
        }


        [Fact]
        public void FormatAsBold_EmptyContent_ShouldThrowArgumentNullException()
        {
            // Arrange

            // Act
            var act = () => formatter.FormatAsBold(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

     
    }
}
