using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using PriceCalculationExercise.Contracts;
using PriceCalculationExercise.Contracts.Offer;
using PriceCalculationExercise.Domain;
using PriceCalculationExercise.Domain.Offer;
using PriceCalculationExercise.Service;
using Xunit;

namespace PriceCalculationExercise.UnitTests
{
    public class PriceCalculationTests
    {
        private readonly IProduct productBread;
        private readonly IProduct productButter;
        private readonly IProduct productMilk;
        private readonly List<IDiscount> discounts;

        public PriceCalculationTests()
        {
            productBread = new Product("Bread", 1.00m);
            productButter = new Product("Butter", 0.80m);
            productMilk = new Product("Milk", 1.15m);

            discounts = new IDiscount[]
            {
                new Discount(
                    new QualifyingItemCondition(productButter, 2),
                    new PercentageOffOutcome(50),
                    new ItemTarget(productBread)),
                new Discount(
                    new QualifyingItemCondition(productMilk, 4),
                    new PercentageOffOutcome(100),
                    new ItemTarget(productMilk))
            }.ToList();
        }

        [Fact]
        public void Should_not_apply_discount_when_basket_does_not_qualify_for_any_discounts()
        {
            // Arrange
            var basketItems = new IBasketItem[]
            {
                new BasketItem(productBread, 1),
                new BasketItem(productButter, 1),
                new BasketItem(productMilk, 1)
            }.ToList();

            var basket = new Mock<IBasket>();
            basket.Setup(p => p.Items).Returns(basketItems);
            basket.Setup(p => p.Discounts).Returns(discounts);

            IPriceCalculator priceCalculator = new PriceCalculator();

            // Act
            var total = priceCalculator.CalculateTotalCost(basket.Object);

            // Assert
            total.Should().Be(2.95m);
        }

        [Fact]
        public void Should_apply_half_price_bread_discount_when_basket_has_two_butter()
        {
            // Arrange
            var basketItems = new IBasketItem[]
            {
                new BasketItem(productBread, 2),
                new BasketItem(productButter, 2)
            }.ToList();

            var basket = new Mock<IBasket>();
            basket.Setup(p => p.Items).Returns(basketItems);
            basket.Setup(p => p.Discounts).Returns(discounts);

            IPriceCalculator priceCalculator = new PriceCalculator();

            // Act
            var total = priceCalculator.CalculateTotalCost(basket.Object);

            // Assert
            total.Should().Be(3.10m);
        }

        [Fact]
        public void Should_apply_free_milk_discount_when_basket_has_four_milk()
        {
            // Arrange
            var basketItems = new IBasketItem[]
            {
                new BasketItem(productMilk, 4)
            }.ToList();

            var basket = new Mock<IBasket>();
            basket.Setup(p => p.Items).Returns(basketItems);
            basket.Setup(p => p.Discounts).Returns(discounts);

            IPriceCalculator priceCalculator = new PriceCalculator();

            // Act
            var total = priceCalculator.CalculateTotalCost(basket.Object);

            // Assert
            total.Should().Be(3.45m);
        }

        [Fact]
        public void Should_apply_free_milk_and_half_price_bread_discount_when_basket_has_eight_milk_and_two_butter()
        {
            // Arrange
            var basketItems = new IBasketItem[]
            {
                new BasketItem(productBread, 1),
                new BasketItem(productButter, 2),
                new BasketItem(productMilk, 8)
            }.ToList();

            var basket = new Mock<IBasket>();
            basket.Setup(p => p.Items).Returns(basketItems);
            basket.Setup(p => p.Discounts).Returns(discounts);

            IPriceCalculator priceCalculator = new PriceCalculator();

            // Act
            var total = priceCalculator.CalculateTotalCost(basket.Object);

            // Assert
            total.Should().Be(9.00m);
        }
    }
}