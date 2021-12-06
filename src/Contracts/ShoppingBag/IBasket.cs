using System.Collections.Generic;
using PriceCalculationExercise.Contracts.Offer;

namespace PriceCalculationExercise.Contracts.ShoppingBag
{
    public interface IBasket
    {
        ICustomer Customer { get; }

        IReadOnlyList<IBasketItem> Items { get; }

        IReadOnlyList<IDiscount> Discounts { get; }

        void AddProduct(IProduct product);
        
        void AddProduct(IProduct product, int quantity);
    }
}