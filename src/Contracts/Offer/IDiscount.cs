using System.Collections.Generic;

namespace PriceCalculationExercise.Contracts.Offer
{
    public interface IDiscount
    {
        IReadOnlyList<IDiscountCondition> Conditions { get; }

        IDiscountOutcome Outcome { get; }

        IDiscountTarget Target { get; }
    }
}