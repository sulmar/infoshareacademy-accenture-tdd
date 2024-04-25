using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class CustomersControllerTests
{
    CustomersController sut;

    public CustomersControllerTests()
    {
        sut = new CustomersController();
    }

    [Fact]
    public void GetCustomer_CustomerIdIsZero_ShouldReturnsNotFound()
    {
        // Arrange

        // Act
        var result = sut.GetCustomer(0);

        // Assert
        Assert.IsType<NotFound>(result);        
    }

    [Fact]
    public void GetCustomer_CustomerIdIsNotZero_ShouldReturnsOk()
    {
        // Arrange

        // Act
        var result = sut.GetCustomer(1);

        // Assert
        Assert.IsType<Ok>(result);
    }
}
