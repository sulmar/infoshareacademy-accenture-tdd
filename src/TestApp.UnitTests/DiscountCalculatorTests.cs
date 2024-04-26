﻿using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using TestApp.Fundamentals;
using Xunit;
using Assert = Xunit.Assert;

namespace TestApp.UnitTests;

public class DiscountCalculatorTests
{
    [Fact]
    public void CalculateDiscount_EmptyDiscountCode_ShouldReturnsOriginalPrice()
    {
        // Arrange
        var sut = new DiscountCalculator();

        // Act
        var result = sut.CalculateDiscount(1, string.Empty);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalculateDiscount_DiscountCodeSAVE10NOW_ShouldReturns10PercentageDiscount()
    {
        // Arrange
        var sut = new DiscountCalculator();

        // Act
        var result = sut.CalculateDiscount(100, "SAVE10NOW");

        // Assert
        Assert.Equal(90, result);
    }

    [Fact]
    public void CalculateDiscount_DiscountCodeDISCOUNT20OFF_ShouldReturns20PercentageDiscount()
    {
        // Arrange
        var sut = new DiscountCalculator();

        // Act
        var result = sut.CalculateDiscount(100, "DISCOUNT20OFF");

        // Assert
        Assert.Equal(80, result);
    }

    [Fact]
    public void CalculateDiscount_PriceIsNegative_ShouldThrowsArgumentException()
    {
        // Arrange
        var sut = new DiscountCalculator();

        // Act
        Action act = () => sut.CalculateDiscount(-1, string.Empty);

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Negatives not allowed", exception.Message);
    }


    [Fact]
    public void CalculateDiscount_InvalidDiscountCode_ShouldThrowsArgumentException()
    {
        // Arrange
        var sut = new DiscountCalculator();

        // Act
        Action act = () => sut.CalculateDiscount(1, "a");

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Invalid discount code", exception.Message);
    }

    [Fact]
    public void CalculateDiscount_FirstSingleUseDiscountCode_ShouldReturns50PercentageDiscount()
    {
        // Arrange
        var sut = new DiscountCalculator();
        sut.AddDiscountCodeToPool("a");

        // Act
        var result = sut.CalculateDiscount(100, "a");

        // Assert
        Assert.Equal(50, result);
    }

    [Fact]
    public void CalculateDiscount_SecondSingleUseDiscountCode_ShouldThrowsArgumentException()
    {
        // Arrange
        var sut = new DiscountCalculator();
        sut.AddDiscountCodeToPool("a");
        sut.CalculateDiscount(100, "a");

        // Act
        Action act = () => sut.CalculateDiscount(100, "a");

        // Assert
        var exception = Assert.Throws<ArgumentException>(act);
        Assert.Equal("Invalid discount code", exception.Message);
    }

    [Fact]
    public void AddDiscountCodeToPool_NotEmptyDiscountCode_DiscountCodePoolHasDiscountCode()
    {
        // Arrange
        var sut = new DiscountCalculator();

        // Act
        sut.AddDiscountCodeToPool("a");

        // Assert
        Assert.Collection(sut.DiscountCodePool, item => item.Contains("a"));
    }

    [Fact]
    public void AddDiscountCodeToPool_TwiceTheSameDiscountCode_ShouldThrowsInvalidOperationException()
    {
        // Arrange
        var sut = new DiscountCalculator();
        sut.AddDiscountCodeToPool("a");

        // Act
        Action act = () => sut.AddDiscountCodeToPool("a");

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void AddDiscountCodeToPool_EmptyDiscountCode_ShouldThrowsArgumentException()
    {
        // Arrange
        var sut = new DiscountCalculator();

        // Act
        Action act = () => sut.AddDiscountCodeToPool(string.Empty);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }
}
