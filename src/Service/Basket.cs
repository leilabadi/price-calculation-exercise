using System;
using System.Collections.Generic;
using PriceCalculationExercise.Contracts;

namespace PriceCalculationExercise.Service
{
    public class Basket : IBasket
    {
        public ICustomer Customer => throw new NotImplementedException();

        public IReadOnlyList<IProduct> Products => throw new NotImplementedException();

        public IReadOnlyList<IDiscount> Discounts => throw new NotImplementedException();

        public void AddProduct(IProduct product, int quantity)
        {
            // immutability
            throw new NotImplementedException();
        }
    }
}