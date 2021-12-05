using System.Collections.Generic;
using PriceCalculationExercise.Contracts.Offer;

namespace PriceCalculationExercise.Contracts
{
    public interface IBasket
    {
        ICustomer Customer { get; }

        IReadOnlyList<IBasketItem> Items { get; }

        IReadOnlyList<IDiscount> Discounts { get; }

        void AddProduct(IProduct product, int quantity);
    }
}