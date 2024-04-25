﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class ProductsControllerTests
{
    [Fact]
    public void Get_FirstCall_ShouldSetCacheHitEqualZero()
    {
        // Arrange
        var productId = 1;
        var sut = new ProductsController(new DbProductRepository(), new CacheProductRepository());

        // Act
        var response = sut.Get(productId);

        // Assert
        Assert.Equal(0,  response.CacheHit);
    }

    [Fact]
    public void Get_SecondCall_ShouldSetCacheHitEqualOne() 
    {
        // Arrange
        var productId = 1;
        var sut = new ProductsController(new DbProductRepository(), new CacheProductRepository());
        sut.Get(productId);

        // Act
        var response = sut.Get(productId);

        // Assert
        Assert.Equal(1, response.CacheHit);
    }
}