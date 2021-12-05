using PriceCalculationExercise.Contracts;
using PriceCalculationExercise.Contracts.Offer;

namespace PriceCalculationExercise.Domain.Offer
{
    public class ItemTarget : IDiscountTarget
    {
        public IProduct Product { get; }

        public ItemTarget(IProduct product)
        {
            Product = product;
        }
    }
}