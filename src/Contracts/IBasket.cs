using System.Collections.Generic;

namespace PriceCalculationExercise.Contracts
{
    public interface IBasket
    {
        ICustomer Customer { get; }

        IReadOnlyList<IProduct> Products { get; }

        IReadOnlyList<IDiscount> Discounts { get; }

        void AddProduct(IProduct product, int quantity);
    }
}