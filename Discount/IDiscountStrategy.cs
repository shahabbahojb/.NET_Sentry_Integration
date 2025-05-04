namespace SampleApp_Review.Discount;

public interface IDiscountStrategy
{
    decimal Apply(decimal total);
}