using System;
using System.Collections.Generic;

namespace TestApp.Fundamentals;

public interface IDiscountFactory
{
    decimal Create(string discountCode);
}

// Wzorzec Proxy (Pośrednik)
public class DiscountFactoryProxy : IDiscountFactory
{
    private readonly HashSet<string> discountCodePool = new HashSet<string>();
    public HashSet<string> DiscountCodePool => discountCodePool;

    private readonly IDiscountFactory discountFactory;

    public DiscountFactoryProxy(IDiscountFactory discountFactory)
    {
        this.discountFactory = discountFactory;
    }

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
        else
            // Real Subject
            return discountFactory.Create(discountCode);

    }
}
