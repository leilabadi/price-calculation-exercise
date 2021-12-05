using PriceCalculationExercise.Contracts.Offer;

namespace PriceCalculationExercise.Domain.Offer
{
    public class Discount : IDiscount
    {
        public IDiscountCondition Condition { get; }

        public IDiscountOutcome Outcome { get; }

        public IDiscountTarget Target { get; }

        public Discount(IDiscountCondition condition, IDiscountOutcome outcome, IDiscountTarget target)
        {
            Condition = condition;
            Outcome = outcome;
            Target = target;
        }
    }
}