using System.Collections.Generic;

namespace PriceCalculationExercise.Contracts.Offer
{
    public interface IDiscount
    {
        IDiscountCondition Condition { get; }

        IDiscountOutcome Outcome { get; }

        IDiscountTarget Target { get; }
    }
}