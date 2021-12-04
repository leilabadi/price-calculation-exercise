using FluentAssertions;
using PriceCalculationExercise.Contracts;
using PriceCalculationExercise.Domain;
using PriceCalculationExercise.Service;
using Xunit;

namespace PriceCalculationExercise.UnitTests
{
    public class PriceCalculationTests
    {
        private readonly IProduct ProductBread;
        private readonly IProduct ProductButter;
        private readonly IProduct ProductMilk;

        public PriceCalculationTests()
        {
            ProductBread = new Product("Bread", 1.00m);
            ProductButter = new Product("Butter", 0.80m);
            ProductMilk = new Product("Milk", 1.15m);
        }

        [Fact]
        public void Should_not_apply_discount_when_basket_does_not_qualify_for_any_discounts()
        {
            // Arrange
            IBasket basket = new Basket();
            basket.AddProduct(ProductBread, 1);
            basket.AddProduct(ProductButter, 1);
            basket.AddProduct(ProductMilk, 1);

            IPriceCalculator priceCalculator = new PriceCalculator();

            // Act
            var total = priceCalculator.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(2.95m);
        }

        [Fact]
        public void Should_apply_half_price_bread_discount_when_basket_has_two_butter()
        {
            // Arrange
            IBasket basket = new Basket();
            basket.AddProduct(ProductBread, 2);
            basket.AddProduct(ProductButter, 2);

            IPriceCalculator priceCalculator = new PriceCalculator();

            // Act
            var total = priceCalculator.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(3.10m);
        }

        [Fact]
        public void Should_apply_free_milk_discount_when_basket_has_four_milk()
        {
            // Arrange
            IBasket basket = new Basket();
            basket.AddProduct(ProductMilk, 4);

            IPriceCalculator priceCalculator = new PriceCalculator();

            // Act
            var total = priceCalculator.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(3.45m);
        }

        [Fact]
        public void Should_apply_free_milk_and_half_price_bread_discount_when_basket_has_eight_milk_and_two_butter()
        {
            // Arrange
            IBasket basket = new Basket();
            basket.AddProduct(ProductBread, 1);
            basket.AddProduct(ProductButter, 2);
            basket.AddProduct(ProductMilk, 8);

            IPriceCalculator priceCalculator = new PriceCalculator();

            // Act
            var total = priceCalculator.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(9.00m);
        }
    }
}