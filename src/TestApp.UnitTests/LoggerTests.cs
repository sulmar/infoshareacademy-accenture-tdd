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
    Logger sut;

    public LoggerTests()
    {
        sut = new Logger();
    }

    [Fact]
    public void Log_MessageIsEmpty_ShouldThrowsArgumentException()
    {
        // Arrange

        // Act
        var act = () => sut.Log(string.Empty);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Log_MessageIsNotEmpty_ShouldSetLastMessage()
    {
        // Arrange

        // Act
        sut.Log("a");

        // Assert
        Assert.Equal("a", sut.LastMessage);
    }
}
