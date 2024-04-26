using Moq;
using TestApp.Mocking;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

// dotnet add package Moq

public class TrackingServiceMockTests
{
    // Arrange
    const string ValidJson = """
                {"Latitude":52.01,"Longitude":20.99}
            """;

    const string InvalidJson = "a";

    Mock<IFileService> mockFileService;
    TrackingService sut;

    public TrackingServiceMockTests()
    {
        mockFileService = new Mock<IFileService>();
        sut = new TrackingService(mockFileService.Object);
    }

    [Fact]
    public void Get_ValidJson_ShouldReturnsLocation()
    {
        // Arrange
        mockFileService
            .Setup(fs => fs.ReadAllText("tracking.txt"))
            .Returns(ValidJson);

        // Act
        var result = sut.Get();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Get_InvalidJson_ShouldThrowFormatException()
    {
        // Arrange
        mockFileService
            .Setup(fs => fs.ReadAllText("tracking.txt"))
            .Returns(InvalidJson);

        // Act
        Action act = () => sut.Get();

        // Assert
        Assert.Throws<FormatException>(act);


    }
}
