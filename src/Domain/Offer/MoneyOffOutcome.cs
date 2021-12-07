using PriceCalculationExercise.Contracts.Offer;

namespace PriceCalculationExercise.Domain.Offer
{
    public class MoneyOffOutcome : IDiscountOutcome
    {
        public decimal Amount { get; }

        public MoneyOffOutcome(decimal amount)
        {
            Amount = amount;
        }
    }
}