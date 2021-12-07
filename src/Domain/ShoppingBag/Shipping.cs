using PriceCalculationExercise.Contracts.ShoppingBag;

namespace PriceCalculationExercise.Domain.ShoppingBag
{
    public class Shipping : IShipping
    {
        public string Name { get; }

        public decimal Price { get; }

        public decimal CalculatedPrice { get; set; }

        public Shipping(string name, decimal price)
        {
            Name = name;
            Price = price;
            CalculatedPrice = price;
        }

        public Shipping(IShipping shipping)
        {
            Name = shipping.Name;
            Price = shipping.Price;
            CalculatedPrice = shipping.CalculatedPrice;
        }
    }
}