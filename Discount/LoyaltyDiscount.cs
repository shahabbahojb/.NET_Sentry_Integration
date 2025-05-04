namespace SampleApp_Review.Discount;

public class LoyaltyDiscount : IDiscountStrategy
{
    public decimal Apply(decimal total) => total * 0.95m;
}