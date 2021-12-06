using PriceCalculationExercise.Contracts;

namespace PriceCalculationExercise.Domain
{
    public class BasketItem : IBasketItem
    {
        public IProduct Product { get; }

        public int Quantity { get; }

        public BasketItem(IProduct product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public BasketItem(IBasketItem basketItem)
        {
            Product = new Product(basketItem.Product);
            Quantity = basketItem.Quantity;
        }
    }
}