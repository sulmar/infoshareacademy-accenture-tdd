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

    [Fact]
    public void Get_ValidJson_ShouldReturnsLocation()
    {
        // Arrange
        Mock<IFileService> mockFileService = new Mock<IFileService>();

        mockFileService
            .Setup(fs => fs.ReadAllText("tracking.txt"))
            .Returns(ValidJson);

        IFileService fileService = mockFileService.Object;
        TrackingService sut = new TrackingService(fileService);

        // Act
        var result = sut.Get();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Get_InvalidJson_ShouldThrowFormatException()
    {
        // Arrange
        Mock<IFileService> mockFileService = new Mock<IFileService>();

        mockFileService
            .Setup(fs => fs.ReadAllText("tracking.txt"))
            .Returns(InvalidJson);

        IFileService fileService = mockFileService.Object;
        TrackingService sut = new TrackingService(fileService);
        
        // Act
        Action act = () => sut.Get();

        // Assert
        Assert.Throws<FormatException>(act);


    }
}
