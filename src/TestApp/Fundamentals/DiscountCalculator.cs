using System;

namespace TestApp.Fundamentals;

public class DiscountCalculator(IDiscountFactory factory)
{    
    public decimal CalculateDiscount(decimal price, string discountCode)
    {
        if (price < 0)
            throw new ArgumentException("Negatives not allowed");

        var discount = factory.Create(discountCode);

        return price - price * discount;
      
    }
}
