namespace SampleApp_Review.Discount;

public class CheckoutService(IEnumerable<IDiscountStrategy> strategies)
{
    public decimal CalculateTotal(decimal basketTotal) =>
        strategies.Aggregate(basketTotal, (t, s) => s.Apply(t));
}