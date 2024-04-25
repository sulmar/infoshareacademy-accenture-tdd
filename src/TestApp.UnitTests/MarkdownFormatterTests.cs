using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests
{
    public class MarkdownFormatterTests
    {
        // Method_Scenario_ExpectedBehavior

        [Fact]
        public void FormatAsBold_NotEmptyContent_ReturnsContentEncloseDoubleAsterix()
        {
            // Arrange
            MarkdownFormatter formatter = new MarkdownFormatter();

            // Act
            var result = formatter.FormatAsBold("a");

            // Assert
            Assert.Equal("**a**", result);
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
