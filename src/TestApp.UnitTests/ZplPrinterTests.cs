using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;
using FluentAssertions;

namespace TestApp.UnitTests;

public class ZplPrinterTests
{

    [Fact]
    public void CreateLabel_ValidSize_ShouldCreateLabel()
    {
        var sut = new ZplPrinter();

        sut.CreateLabel(20, 10);

        sut.Output.Should()
         .StartWith("^XA")
         .And.EndWith("^XZ");
    }

    [Fact]
    public void SetText_NotEmptyText_ShouldReturnCommandText()
    {
        var sut = new ZplPrinter();
        sut.CreateLabel(20, 10);

        sut.SetText("Hello World");

        sut.Output.Should()
            .StartWith("^XA")
            .And.Contain("^FDHello World^FS")
            .And.EndWith("^XZ");

    }

    [Fact]
    public void SetText_EmptyText_ShouldThrowsArgumentNullException()
    {
        var sut = new ZplPrinter();

        Action act = () => sut.SetText(string.Empty);

        act.Should().Throw<ArgumentNullException>();    
    }

    [Fact]
    public void SetPosition_ValidPosition_ShouldReturnCommandText()
    {
        var sut = new ZplPrinter();
        sut.CreateLabel(20, 10);

        sut.SetPosition(1, 2);

        sut.Output.Should()
            .StartWith("^XA")
            .And.Contain("^FO1,2")
            .And.EndWith("^XZ");

    }
}
