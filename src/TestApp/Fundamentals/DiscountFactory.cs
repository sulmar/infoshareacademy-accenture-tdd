using System;
using System.Collections.Generic;

namespace TestApp.Fundamentals;

// Wzorzec Proxy (Pośrednik)
public class DiscountFactoryProxy(IDiscountFactory discountFactory) : IDiscountFactory
{
    private readonly HashSet<string> discountCodePool = [];
    public HashSet<string> DiscountCodePool => discountCodePool;

    public void AddDiscountCodeToPool(string discountCode)
    {
        ArgumentException.ThrowIfNullOrEmpty(discountCode);

        if (!discountCodePool.Add(discountCode))
            throw new InvalidOperationException();
    }

    public decimal Create(string discountCode)
    {
        if (discountCodePool.Remove(discountCode))
            return 0.5m;
        else
            // Real Subject
            return discountFactory.Create(discountCode);

    }
}
