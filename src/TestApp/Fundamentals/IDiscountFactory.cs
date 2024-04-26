namespace TestApp.Fundamentals;

public interface IDiscountFactory
{
    decimal Create(string discountCode);
}
