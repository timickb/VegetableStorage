namespace VegetableStorage
{
    public class Box
    {
        
        private readonly int _priceForKilo;

        public int PriceForKilo => _priceForKilo;

        public int Weight { get; }

        public Box(int weight, int priceForKilo)
        {
            Weight = weight;
            _priceForKilo = priceForKilo;
        }

    }
}