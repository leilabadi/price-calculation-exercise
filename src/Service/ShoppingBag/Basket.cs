using System;
using System.Collections.Generic;
using PriceCalculationExercise.Contracts;
using PriceCalculationExercise.Contracts.Offer;
using PriceCalculationExercise.Contracts.ShoppingBag;
using PriceCalculationExercise.Domain;
using PriceCalculationExercise.Domain.ShoppingBag;

namespace PriceCalculationExercise.Service.ShoppingBag
{
    public class Basket : IBasket
    {
        public ICustomer Customer { get; }

        private readonly List<IBasketItem> items;
        public IReadOnlyList<IBasketItem> Items => items;

        public IShipping Shipping { get; }

        private readonly List<IDiscount> discounts;
        public IReadOnlyList<IDiscount> Discounts => discounts;

        public Basket(ICustomer customer, IShipping shipping, List<IDiscount> discounts)
        {
            Customer = customer;
            this.items = new List<IBasketItem>();
            this.Shipping = shipping;
            this.discounts = discounts ?? throw new Exception("The discounts can not be null. Use the constructor without discount.");
        }

        public Basket(ICustomer customer, IShipping shipping) : this(customer, shipping, new List<IDiscount>())
        {
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