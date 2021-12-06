using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using PriceCalculationExercise.Contracts;
using PriceCalculationExercise.Contracts.Offer;
using PriceCalculationExercise.Domain;
using PriceCalculationExercise.Domain.Offer;
using PriceCalculationExercise.Service;
using PriceCalculationExercise.Service.ShoppingBag;
using Xunit;

namespace PriceCalculationExercise.IntegrationTests
{
    public class PriceCalculationTests
    {
        private readonly Mock<ICustomer> customerMock;
        private readonly List<IDiscount> discounts;
        private readonly IProduct productBread;
        private readonly IProduct productButter;
        private readonly IProduct productMilk;

        public PriceCalculationTests()
        {
            customerMock = new Mock<ICustomer>();

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
            var basket = new Basket(customerMock.Object, discounts);
            basket.AddProduct(new Product(productBread), 1);
            basket.AddProduct(new Product(productButter), 1);
            basket.AddProduct(new Product(productMilk), 1);

            var sut = new PriceCalculator();

            // Act
            var total = sut.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(2.95m);
        }

        [Fact]
        public void Should_apply_half_price_bread_discount_when_basket_has_two_butter()
        {
            // Arrange
            var basket = new Basket(customerMock.Object, discounts);
            basket.AddProduct(new Product(productBread), 2);
            basket.AddProduct(new Product(productButter), 2);

            var sut = new PriceCalculator();

            // Act
            var total = sut.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(3.10m);
        }

        [Fact]
        public void Should_apply_free_milk_discount_when_basket_has_four_milk()
        {
            // Arrange
            var basket = new Basket(customerMock.Object, discounts);
            basket.AddProduct(new Product(productMilk), 4);

            var sut = new PriceCalculator();

            // Act
            var total = sut.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(3.45m);
        }

        [Fact]
        public void Should_apply_free_milk_and_half_price_bread_discount_when_basket_has_eight_milk_and_two_butter()
        {
            // Arrange
            var basket = new Basket(customerMock.Object, discounts);
            basket.AddProduct(new Product(productBread), 1);
            basket.AddProduct(new Product(productButter), 2);
            basket.AddProduct(new Product(productMilk), 8);

            var sut = new PriceCalculator();

            // Act
            var total = sut.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(9.00m);
        }
    }
}