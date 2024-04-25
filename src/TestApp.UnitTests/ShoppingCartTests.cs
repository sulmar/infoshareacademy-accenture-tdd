using Xunit;
using FluentAssertions;

namespace TestApp.UnitTests;

public class ShoppingCartTests
{
    ShoppingCart sut;
    CartItem ValidCartItem;

    public ShoppingCartTests()
    {
        sut = new ShoppingCart();
        ValidCartItem = new CartItem("a", 1, 1);
    }

    [Fact]
    public void AddItem_EmptyCartItem_ShouldThrowsArgumentNullException()
    {
        var act = () => sut.AddItem(null);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddItem_CartItem_ShouldContainsOneCartItem()
    {
        sut.AddItem(ValidCartItem);

        sut.GetItems().Should().ContainSingle();
    }

    [Fact]
    public void RemoveItem_CartItem_ShouldShoppingCartEmpty()
    {
        sut.AddItem(ValidCartItem);

        var result = sut.RemoveItem(ValidCartItem);

        result.Should().BeTrue();
        sut.GetItems().Should().BeEmpty();
    }

    [Fact]
    public void RemoveItem_EmptyCartItem_ShouldThrowsArgumentNullException()
    {
        var act = () => sut.RemoveItem(null);

        act.Should().Throw<ArgumentNullException>();

    }

    [Fact]
    public void GetItems_WhenShoppingCartEmpty_ShouldReturnsEmpty()
    {
        ShoppingCart sut = new ShoppingCart();

        sut.GetItems().Should().BeEmpty();
    }

    [Fact]
    public void GetItems_FirstAddCartItem_ShouldReturnsOneCartItem()
    {
        sut.AddItem(ValidCartItem);

        sut.GetItems().Should().ContainSingle();
    }

    [Fact]
    public void CalculateTotal_EmptyShoppingCart_ShouldReturnTotalAmountEqualZero()
    {
        var result = sut.CalculateTotal();

        result.Should().Be(decimal.Zero);
    }

    [Fact]
    public void CalculateTotal_NotEmptyShoppingCart_ShouldReturnTotalAmountGreatherThanZero()
    {
        sut.AddItem(new CartItem("a", 1, 2));

        var result = sut.CalculateTotal();

        result.Should().BeGreaterThan(decimal.Zero);
        result.Should().Be(2);
    }




}
