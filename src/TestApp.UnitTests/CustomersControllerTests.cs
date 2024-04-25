using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class CustomersControllerTests
{

    [Fact]
    public void GetCustomer_CustomerIdIsZero_ShouldReturnsNotFound()
    {
        // Arrange
        CustomersController customersController = new CustomersController();

        // Act
        var result = customersController.GetCustomer(0);

        // Assert
        Assert.IsType<NotFound>(result);
        
    }

    [Fact]
    public void GetCustomer_CustomerIdIsNotZero_ShouldReturnsOk()
    {
        // Arrange
        CustomersController customersController = new CustomersController();

        // Act
        var result = customersController.GetCustomer(1);

        // Assert
        Assert.IsType<Ok>(result);
    }
}
