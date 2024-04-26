using System;

namespace TestApp.Fundamentals;

public class PernamentDiscountFactory : IDiscountFactory
{
    public decimal Create(string discountCode)
    {
        switch (discountCode)
        {
            case "": return 0;
            case "SAVE10NOW": return 0.1m;
            case "DISCOUNT20OFF": return 0.2m;
            default: throw new ArgumentException("Invalid discount code");
        }
    }
}
