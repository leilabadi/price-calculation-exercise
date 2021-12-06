using System.Collections.Generic;
using PriceCalculationExercise.Contracts;
using PriceCalculationExercise.Contracts.Offer;
using PriceCalculationExercise.Domain;

namespace PriceCalculationExercise.Service
{
    public class Basket : IBasket
    {
        public ICustomer Customer { get; }

        private readonly List<IBasketItem> items;
        public IReadOnlyList<IBasketItem> Items => items;

        private readonly List<IDiscount> discounts;
        public IReadOnlyList<IDiscount> Discounts => discounts;

        public Basket(ICustomer customer, List<IDiscount> discounts)
        {
            Customer = customer;
            this.items = new List<IBasketItem>();
            this.discounts = discounts;
        }

        public void AddProduct(IProduct product)
        {
            items.Add(new BasketItem(new Product(product)));
        }

        public void AddProduct(IProduct product, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                AddProduct(product);
            }
        }
    }
}