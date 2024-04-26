using System;

namespace TestApp.Fundamentals;

public class PernamentDiscountFactory : IDiscountFactory
{
    public decimal Create(string discountCode) => discountCode switch
    {
        "" => 0,
        "SAVE10NOW" => 0.1m,
        "DISCOUNT20OFF" => 0.2m,
        _ => throw new ArgumentException("Invalid discount code"),
    };
}
