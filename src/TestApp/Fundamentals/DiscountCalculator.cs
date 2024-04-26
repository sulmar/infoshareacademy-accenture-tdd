using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Fundamentals;

public class DiscountCalculator
{
    private readonly DiscountFactory factory;

    public DiscountCalculator(DiscountFactory factory)
    {
        this.factory = factory;
    }

    public decimal CalculateDiscount(decimal price, string discountCode)
    {
        if (price < 0)
            throw new ArgumentException("Negatives not allowed");

        var discount = factory.Create(discountCode);

        return price - price * discount;
      
    }
}
