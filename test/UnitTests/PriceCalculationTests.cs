using Xunit;

namespace PriceCalculationExercise.UnitTests
{
    public class PriceCalculationTests
    {
        [Fact]
        public void Should_not_apply_discount_when_basket_does_not_qualify_for_any_discounts()
        {
            // 1 bread
            // 1 butter
            // 1 milk
        }

        [Fact]
        public void Should_apply_half_price_bread_discount_when_basket_has_two_butter()
        {
            // 2 bread
            // 2 butter
        }

        [Fact]
        public void Should_apply_free_milk_discount_when_basket_has_four_milk()
        {
            // 4 milk
        }

        [Fact]
        public void Should_apply_free_milk_and_half_price_bread_discount_when_basket_has_eight_milk_and_two_butter()
        {
            // 1 bread
            // 2 butter
            // 8 milk
        }
    }
}