namespace SampleApp_Review.Discount;

public class ChristmasDiscount : IDiscountStrategy
{
    public decimal Apply(decimal total) => total * 0.90m;
}