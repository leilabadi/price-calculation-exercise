using PriceCalculationExercise.Contracts;

namespace PriceCalculationExercise.Domain
{
    public class Product : IProduct
    {
        public string Name { get; }

        public decimal Price { get; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public Product(IProduct product)
        {
            Name = product.Name;
            Price = product.Price;
        }

        protected bool Equals(Product other)
        {
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Product) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}