using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class PostServiceTests
{
    [Fact]
    public async Task FetchData_WhenCalled_ShouldReturnsResponse()
    {
        // Arrange
        PostService sut = new PostService();

        // Act
        var result = await sut.FetchData("http://a.com");

        // Assets
        Assert.NotNull(result);
    }
}
