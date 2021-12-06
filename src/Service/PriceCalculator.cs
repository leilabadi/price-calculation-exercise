using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PriceCalculationExercise.Contracts;
using PriceCalculationExercise.Contracts.Offer;
using PriceCalculationExercise.Domain;
using PriceCalculationExercise.Domain.Offer;

namespace PriceCalculationExercise.Service
{
    public class PriceCalculator : IPriceCalculator
    {
        public decimal CalculateTotalCost(IBasket basket)
        {
            var dictionary = CopyItemsToDictionary(basket);

            foreach (var offer in basket.Discounts)
            {
                var outcomeFunc = GenerateOutcomeFunc(offer);

                switch (offer.Condition)
                {
                    case QualifyingItemCondition qualifyingItem:
                        foreach (var (product, list) in dictionary)
                        {
                            if (!qualifyingItem.Product.Equals(product)) continue;

                            var numberOfMatches = list.Count / qualifyingItem.Count;
                            switch (offer.Target)
                            {
                                case ItemTarget offerTarget:
                                    dictionary[offerTarget.Product]
                                        .Where(x => !x.DiscountApplied)
                                        .Take(numberOfMatches)
                                        .ToList()
                                        .ForEach(p => p.CalculatedPrice -= outcomeFunc(p.Product.Price));

                                    break;
                                default:
                                    throw new Exception("Unsupported discount target");
                            }
                        }

                        break;
                    default:
                        throw new Exception("Unsupported discount condition");
                }
            }

            var total = basket.Items.Sum(x => x.CalculatedPrice);
            return total;
        }

        private static Func<decimal, decimal> GenerateOutcomeFunc(IDiscount offer)
        {
            Func<decimal, decimal> outcomeFunc;
            switch (offer.Outcome)
            {
                case PercentageOffOutcome percentageOffOutcome:
                    outcomeFunc = price => price * percentageOffOutcome.Percentage / 100m;
                    break;
                default:
                    throw new Exception("Unsupported discount outcome");
            }

            return outcomeFunc;
        }

        private static IReadOnlyDictionary<IProduct, IReadOnlyList<IBasketItem>> CopyItemsToDictionary(IBasket basket)
        {
            var dictionary1 = new Dictionary<IProduct, List<IBasketItem>>();
            foreach (var item in basket.Items)
            {
                if (!dictionary1.ContainsKey(item.Product))
                {
                    dictionary1[item.Product] = new List<IBasketItem> { item };
                }
                else
                {
                    dictionary1[item.Product].Add(item);
                }
            }

            var dictionary2 = new Dictionary<IProduct, IReadOnlyList<IBasketItem>>();
            foreach (var (key, value) in dictionary1)
            {
                dictionary2.Add(key, value);
            }

            return new ReadOnlyDictionary<IProduct, IReadOnlyList<IBasketItem>>(dictionary2);
        }
    }
}