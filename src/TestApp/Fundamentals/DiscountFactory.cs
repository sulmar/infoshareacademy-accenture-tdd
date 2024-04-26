using System;
using System.Collections.Generic;

namespace TestApp.Fundamentals;

public class DiscountFactory
{
    private readonly HashSet<string> discountCodePool = new HashSet<string>();
    public HashSet<string> DiscountCodePool => discountCodePool;

    public void AddDiscountCodeToPool(string discountCode)
    {
        if (string.IsNullOrEmpty(discountCode))
            throw new ArgumentException();

        if (!discountCodePool.Add(discountCode))
            throw new InvalidOperationException();
    }

    public decimal Create(string discountCode)
    {
        if (discountCodePool.Remove(discountCode))
            return 0.5m;

        switch (discountCode)
        {
            case "": return 0;
            case "SAVE10NOW": return 0.1m;
            case "DISCOUNT20OFF": return 0.2m;
            default: throw new ArgumentException("Invalid discount code");
        }
    }
}
