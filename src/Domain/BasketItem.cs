using PriceCalculationExercise.Contracts;

namespace PriceCalculationExercise.Domain
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