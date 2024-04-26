using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Fundamentals;

public class DiscountCalculator
{
    private readonly List<string> discountCodePool = new List<string>();

    public List<string> DiscountCodePool => discountCodePool;

    public void AddDiscountCodeToPool(string discountCode)
    {
        if (string.IsNullOrEmpty(discountCode))
            throw new ArgumentException();

        if (discountCodePool.Contains(discountCode))
            throw new InvalidOperationException();

       discountCodePool.Add(discountCode);
    }

    public decimal CalculateDiscount(decimal price, string discountCode)
    {
        if (price < 0)
            throw new ArgumentException("Negatives not allowed");

        if (string.IsNullOrEmpty(discountCode))
            return price;

        if (discountCode == "SAVE10NOW")
            return price - price * 0.1m;

        if (discountCode == "DISCOUNT20OFF")
            return price - price * 0.2m;

        if (discountCodePool.Contains(discountCode))
        {
            discountCodePool.Remove(discountCode);

            return price - price * 0.5m;
        }

        throw new ArgumentException("Invalid discount code");
    }
}
