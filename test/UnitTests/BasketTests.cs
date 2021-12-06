using System;
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

namespace PriceCalculationExercise.UnitTests
{
    public class BasketTests
    {
        private readonly Mock<ICustomer> customerMock;
        private readonly IProduct productBread;
        private readonly IProduct productButter;
        private readonly IProduct productMilk;

        public BasketTests()
        {
            customerMock = new Mock<ICustomer>();

            productBread = new Product("Bread", 1.00m);
            productButter = new Product("Butter", 0.80m);
            productMilk = new Product("Milk", 1.15m);
        }

        [Fact]
        public void Should_throw_exception_when_null_discounts_is_passed_to_basket()
        {
            // Arrange

            // Act
            Action instantiation = () => new Basket(customerMock.Object, null);

            // Assert
            instantiation.Should().Throw<Exception>().WithMessage("The discounts can not be null. Use the constructor without discount.");
        }

        [Fact]
        public void Should_contain_the_same_items_which_is_added_to_it()
        {
            // Arrange
            var basket = new Basket(customerMock.Object);

            // Act
            basket.AddProduct(productBread);
            basket.AddProduct(productButter);
            basket.AddProduct(productMilk);

            // Assert
            basket.Items.Should().HaveCount(3);
            basket.Items.Select(p => p.Product).Should().Contain(productBread);
            basket.Items.Select(p => p.Product).Should().Contain(productButter);
            basket.Items.Select(p => p.Product).Should().Contain(productMilk);
        }

        [Fact]
        public void Should_contain_two_item_of_the_same_product_when_a_product_is_added_with_2_quantities()
        {
            // Arrange
            var basket = new Basket(customerMock.Object);

            // Act
            basket.AddProduct(productBread, 2);

            // Assert
            basket.Items.Should().HaveCount(2);
            basket.Items.Select(p => p.Product).Should().AllBeEquivalentTo(productBread);
        }
    }
}