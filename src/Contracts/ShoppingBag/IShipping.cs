namespace PriceCalculationExercise.Contracts.ShoppingBag
{
    public interface IShipping
    {
        string Name { get; }

        decimal Price { get; }

        decimal CalculatedPrice { get; set; }
    }
}