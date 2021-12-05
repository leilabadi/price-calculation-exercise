using System.Collections.Generic;

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