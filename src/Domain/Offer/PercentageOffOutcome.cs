using PriceCalculationExercise.Contracts.Offer;

namespace PriceCalculationExercise.Domain.Offer
{
    public class PercentageOffOutcome : IDiscountOutcome
    {
        public int Percentage { get; }

        public PercentageOffOutcome(int percentage)
        {
            Percentage = percentage;
        }
    }
}