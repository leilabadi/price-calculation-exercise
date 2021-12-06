namespace PriceCalculationExercise.Contracts.ShoppingBag
{
    public interface IBasketItem
    {
        IProduct Product { get; }

        decimal CalculatedPrice { get; set; }

        bool DiscountApplied { get; }
    }
}