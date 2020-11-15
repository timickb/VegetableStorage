namespace VegetableStorage
{
    public class Box
    {

        public double PriceForKilo { get; set; }

        public int Weight { get; }

        public double TotalPrice => PriceForKilo * Weight;

        public Box(int weight, double priceForKilo)
        {
            Weight = weight;
            PriceForKilo = priceForKilo;
        }

    }
}