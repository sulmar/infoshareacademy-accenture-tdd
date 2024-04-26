using FakeItEasy;
using TestApp.Mocking;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

// dotnet add package FakeItEasy

public class TrackingServiceFakeItEasyTests
{
    // Arrange
    const string ValidJson = """
                {"Latitude":52.01,"Longitude":20.99}
            """;

    const string InvalidJson = "a";

    readonly IFileService fileService;
    readonly TrackingService sut;

    public TrackingServiceFakeItEasyTests()
    {
        fileService = A.Fake<IFileService>();
        sut = new TrackingService(fileService);
    }

    [Fact]
    public void Get_ValidJson_ShouldReturnsLocation()
    {
        // Arrange
        A.CallTo(() => fileService.ReadAllText(A<string>.Ignored))
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
        A.CallTo(() => fileService.ReadAllText(A<string>.Ignored))
            .Returns(InvalidJson);

        // Act
        Action act = () => sut.Get();

        // Assert
        Assert.Throws<FormatException>(act);


    }
}
