using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using PriceCalculationExercise.Contracts;
using PriceCalculationExercise.Contracts.Offer;
using PriceCalculationExercise.Contracts.ShoppingBag;
using PriceCalculationExercise.Domain;
using PriceCalculationExercise.Domain.Offer;
using PriceCalculationExercise.Domain.ShoppingBag;
using PriceCalculationExercise.Service;
using PriceCalculationExercise.Service.ShoppingBag;
using Xunit;

namespace PriceCalculationExercise.IntegrationTests
{
    public class PriceCalculationTests
    {
        private const string shippingName = "Shipping";
        private readonly Mock<ICustomer> customerMock;
        private readonly IShipping shipping;
        private readonly List<IDiscount> discounts;
        private readonly IProduct productBread;
        private readonly IProduct productButter;
        private readonly IProduct productMilk;

        public PriceCalculationTests()
        {
            customerMock = new Mock<ICustomer>();

            shipping = new Shipping(shippingName, 0);

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
            var basket = new Basket(customerMock.Object, shipping, discounts);
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
            var basket = new Basket(customerMock.Object, shipping, discounts);
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
            var basket = new Basket(customerMock.Object, shipping, discounts);
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
            var basket = new Basket(customerMock.Object, shipping, discounts);
            basket.AddProduct(new Product(productBread), 1);
            basket.AddProduct(new Product(productButter), 2);
            basket.AddProduct(new Product(productMilk), 8);

            var sut = new PriceCalculator();

            // Act
            var total = sut.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(9.00m);
        }

        [Fact]
        public void Money_off_item_discount()
        {
            // Arrange
            var discounts = new List<IDiscount>(new[]
            {
                new Discount(new QualifyingItemCondition(productMilk, 2),
                    new MoneyOffOutcome(0.10m),
                    new ItemTarget(productMilk))
            });

            var basket = new Basket(customerMock.Object, shipping, discounts);
            basket.AddProduct(new Product(productButter), 1);
            basket.AddProduct(new Product(productMilk), 2);

            var sut = new PriceCalculator();

            // Act
            var total = sut.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(3.00m);
        }

        [Fact]
        public void Free_item_discount()
        {
            // Arrange
            var discounts = new List<IDiscount>(new[]
            {
                new Discount(new QualifyingItemCondition(productMilk, 2),
                    new FreeItemOutcome(),
                    new ItemTarget(productMilk))
            });

            var basket = new Basket(customerMock.Object, shipping, discounts);
            basket.AddProduct(new Product(productButter), 1);
            basket.AddProduct(new Product(productMilk), 2);

            var sut = new PriceCalculator();

            // Act
            var total = sut.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(1.95m);
        }

        [Fact]
        public void Complex_discount()
        {
        }

        [Fact]
        public void Free_shipping_discount()
        {
            // Arrange
            var shipping = new Shipping(shippingName, 2);
            
            var discounts = new List<IDiscount>(new[]
            {
                new Discount(new QualifyingItemCondition(productMilk, 2),
                    new FreeItemOutcome(),
                    new ShippingTarget())
            });

            var basket = new Basket(customerMock.Object, shipping, discounts);
            basket.AddProduct(new Product(productButter), 1);
            basket.AddProduct(new Product(productMilk), 2);

            var sut = new PriceCalculator();

            // Act
            var total = sut.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(3.10m);
        }

        [Fact]
        public void Percentage_off_shipping_discount()
        {
            // Arrange
            var shipping = new Shipping(shippingName, 2);
            
            var discounts = new List<IDiscount>(new[]
            {
                new Discount(new QualifyingItemCondition(productMilk, 2),
                    new PercentageOffOutcome(50),
                    new ShippingTarget())
            });

            var basket = new Basket(customerMock.Object, shipping, discounts);
            basket.AddProduct(new Product(productButter), 1);
            basket.AddProduct(new Product(productMilk), 2);

            var sut = new PriceCalculator();

            // Act
            var total = sut.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(4.10m);
        }

        [Fact]
        public void Money_off_shipping_discount()
        {
            // Arrange
            var shipping = new Shipping(shippingName, 2);
            
            var discounts = new List<IDiscount>(new[]
            {
                new Discount(new QualifyingItemCondition(productMilk, 2),
                    new MoneyOffOutcome(0.70m),
                    new ShippingTarget())
            });

            var basket = new Basket(customerMock.Object, shipping, discounts);
            basket.AddProduct(new Product(productButter), 1);
            basket.AddProduct(new Product(productMilk), 2);

            var sut = new PriceCalculator();

            // Act
            var total = sut.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(4.40m);
        }

        [Fact]
        public void Money_off_bag_discount()
        {
            // Arrange
            var discounts = new List<IDiscount>(new[]
            {
                new Discount(new QualifyingItemCondition(productMilk, 2),
                    new MoneyOffOutcome(0.50m),
                    new BasketTarget())
            });

            var basket = new Basket(customerMock.Object, shipping, discounts);
            basket.AddProduct(new Product(productButter), 1);
            basket.AddProduct(new Product(productMilk), 2);

            var sut = new PriceCalculator();

            // Act
            var total = sut.CalculateTotalCost(basket);

            // Assert
            total.Should().Be(2.60m);
        }

        [Fact]
        public void Percentage_off_item_discount_with_multiple_condition()
        {
        }
    }
}