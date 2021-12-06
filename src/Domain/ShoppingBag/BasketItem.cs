using PriceCalculationExercise.Contracts;
using PriceCalculationExercise.Contracts.ShoppingBag;

namespace PriceCalculationExercise.Domain.ShoppingBag
{
    public class BasketItem : IBasketItem
    {
        public IProduct Product { get; }

        public decimal CalculatedPrice { get; set; }

        public bool DiscountApplied => CalculatedPrice != Product.Price;

        public BasketItem(IProduct product)
        {
            Product = product;
            CalculatedPrice = product.Price;
        }

        public BasketItem(IBasketItem basketItem)
        {
            Product = new Product(basketItem.Product);
            CalculatedPrice = basketItem.CalculatedPrice;
        }
    }
}