using System.Collections.Generic;

namespace VegetableStorage
{
    public class Container
    {
        private readonly int _maxWeight;
        private List<Box> _boxes;
        public int MaxWeight => _maxWeight;
        public Container(int maxWeight, List<Box> boxes)
        {
            _maxWeight = maxWeight;
            _boxes = boxes;
        }
    }
}