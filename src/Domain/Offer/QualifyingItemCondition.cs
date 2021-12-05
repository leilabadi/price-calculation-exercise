using PriceCalculationExercise.Contracts;
using PriceCalculationExercise.Contracts.Offer;

namespace PriceCalculationExercise.Domain.Offer
{
    public class QualifyingItemCondition : IDiscountCondition
    {
        public IProduct Product { get; set; }

        public int Count { get; set; }

        public QualifyingItemCondition(IProduct product, int count)
        {
            Product = product;
            Count = count;
        }
    }
}