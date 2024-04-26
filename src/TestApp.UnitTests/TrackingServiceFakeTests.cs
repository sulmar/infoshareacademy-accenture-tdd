using TestApp.Mocking;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class FakeValidFileService : IFileService
{
    public string ReadAllText(string path)
    {
        string json = """
                {"Latitude":52.01,"Longitude":20.99}
            """;

        return json;
    }
}

public class FakeInvalidFileService : IFileService
{
    public string ReadAllText(string path) => "a";
}

public class TrackingServiceFakeTests
{
    [Fact]
    public void Get_ValidJson_ShouldReturnsLocation()
    {
        // Arrange
        TrackingService sut = new TrackingService(new FakeValidFileService());

        // Act
        var result = sut.Get();

        // Assert
        Assert.NotNull(result);

    }

    [Fact]
    public void Get_InvalidJson_ShouldThrowFormatException() 
    {
        // Arrange
        TrackingService sut = new TrackingService(new FakeInvalidFileService());

        // Act
        Action act = () => sut.Get();

        // Assert
        Assert.Throws<FormatException>(act);    
    }
}
