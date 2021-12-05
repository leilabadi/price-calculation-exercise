namespace PriceCalculationExercise.Contracts
{
    public interface IBasketItem
    {
        IProduct Product { get; }

        int Quantity { get; }
    }
}