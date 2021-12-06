using FluentAssertions;
using Moq;
using PriceCalculationExercise.Contracts;
using PriceCalculationExercise.Domain;
using PriceCalculationExercise.Service.ShoppingBag;
using Xunit;

namespace PriceCalculationExercise.UnitTests
{
    public class BasketItemTests
    {
        private readonly Mock<ICustomer> customerMock;

        public BasketItemTests()
        {
            customerMock = new Mock<ICustomer>();
        }

        [Fact]
        public void DiscountApplied_flag_should_return_true_when_calculated_price_is_set_to_different_price_from_product_price()
        {
            // Arrange
            var basket = new Basket(customerMock.Object);
            basket.AddProduct(new Product("Bread", 1));

            // Act
            basket.Items[0].CalculatedPrice = 2;

            // Assert

            basket.Items[0].DiscountApplied.Should().BeTrue();
        }

        [Fact]
        public void DiscountApplied_flag_should_return_false_when_calculated_price_is_set_to_the_same_price_as_product_price()
        {
            // Arrange
            var basket = new Basket(customerMock.Object);
            basket.AddProduct(new Product("Bread", 1));

            // Act
            basket.Items[0].CalculatedPrice = 1;

            // Assert

            basket.Items[0].DiscountApplied.Should().BeFalse();
        }
    }
}