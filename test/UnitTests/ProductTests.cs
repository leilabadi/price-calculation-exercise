using FluentAssertions;
using PriceCalculationExercise.Domain;
using Xunit;

namespace PriceCalculationExercise.UnitTests
{
    public class ProductTests
    {
        [Fact]
        public void A_product_should_be_equal_to_itself()
        {
            // Arrange
            var p = new Product("Bread", 0);

            // Act
            var result = p.Equals(p);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Two_product_with_the_same_names_should_be_equal()
        {
            // Arrange
            var p1 = new Product("Bread", 0);
            var p2 = new Product("Bread", 1);

            // Act
            var result = p1.Equals(p2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Two_product_with_different_names_should_not_be_equal()
        {
            // Arrange
            var p1 = new Product("Bread", 0);
            var p2 = new Product("Butter", 1);

            // Act
            var result = p1.Equals(p2);

            // Assert
            result.Should().BeFalse();
        }
    }
}