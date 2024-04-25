using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class LoggerTests
{
    [Fact]
    public void Log_MessageIsEmpty_ShouldThrowsArgumentNullException()
    {
        // Arrange
        Logger sut = new Logger();

        // Act
        var act = () => sut.Log(string.Empty);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void Log_MessageIsNotEmpty_ShouldSetLastMessage()
    {
        // Arrange
        Logger sut = new Logger();

        // Act
        sut.Log("a");

        // Assert
        Assert.Equal("a", sut.LastMessage);
    }
}
