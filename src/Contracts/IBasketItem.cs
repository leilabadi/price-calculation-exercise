namespace PriceCalculationExercise.Contracts
{
    public interface IBasketItem
    {
        IProduct Product { get; }

        decimal CalculatedPrice { get; set; }

        bool DiscountApplied { get; }
    }
}