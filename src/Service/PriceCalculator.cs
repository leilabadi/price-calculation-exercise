using System.Linq;
using PriceCalculationExercise.Contracts;

namespace PriceCalculationExercise.Service
{
    public class PriceCalculator : IPriceCalculator
    {
        public decimal CalculateTotalCost(IBasket basket)
        {
            return basket.Items.Sum(p => p.Quantity * p.Product.Price);
        }
    }
}