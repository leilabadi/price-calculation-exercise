namespace PriceCalculationExercise.Contracts
{
    public interface IPriceCalculator
    {
        decimal CalculateTotalCost(IBasket basket);
    }
}
