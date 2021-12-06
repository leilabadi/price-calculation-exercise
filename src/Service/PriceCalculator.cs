using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PriceCalculationExercise.Contracts;
using PriceCalculationExercise.Domain;
using PriceCalculationExercise.Domain.Offer;

namespace PriceCalculationExercise.Service
{
    public class PriceCalculator : IPriceCalculator
    {
        public decimal CalculateTotalCost(IBasket basket)
        {
            var dictionary = CopyItemsToDictionary(basket);
            var basketItems = basket.Items.ToList().ConvertAll(item => new BasketItem(item));

            var total = basket.Items.Sum(x => x.Quantity * x.Product.Price);

            foreach (var offer in basket.Discounts)
            {
                var qualifyingItem = (QualifyingItemCondition)offer.Condition;

                foreach (var (product, quantity) in dictionary)
                {
                    if (!qualifyingItem.Product.Equals(product)) continue;

                    var remaining = quantity;
                    while (remaining - qualifyingItem.Count >= 0)
                    {
                        remaining -= qualifyingItem.Count;

                        var offerTarget = (ItemTarget)offer.Target;
                        var targetItem = basketItems.SingleOrDefault(x => x.Product.Equals(offerTarget.Product));
                        if (targetItem == null) continue;

                        var percentage = ((PercentageOffOutcome)offer.Outcome).Percentage;
                        total -= targetItem.Product.Price * percentage / 100m;
                    }
                }
            }

            return total;
        }

        private static IReadOnlyDictionary<IProduct, int> CopyItemsToDictionary(IBasket basket)
        {
            var pairs = basket.Items.ToList().ConvertAll(p => new KeyValuePair<IProduct, int>(new Product(p.Product), p.Quantity));

            var dictionary = new Dictionary<IProduct, int>(pairs);

            return new ReadOnlyDictionary<IProduct, int>(dictionary);
        }
    }
}