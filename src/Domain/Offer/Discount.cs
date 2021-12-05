using System.Collections.Generic;
using PriceCalculationExercise.Contracts.Offer;

namespace PriceCalculationExercise.Domain.Offer
{
    public class Discount : IDiscount
    {
        public IReadOnlyList<IDiscountCondition> Conditions { get; }

        public IDiscountOutcome Outcome { get; }

        public IDiscountTarget Target { get; }

        public Discount(IReadOnlyList<IDiscountCondition> conditions, IDiscountOutcome outcome, IDiscountTarget target)
        {
            Conditions = conditions;
            Outcome = outcome;
            Target = target;
        }
    }
}