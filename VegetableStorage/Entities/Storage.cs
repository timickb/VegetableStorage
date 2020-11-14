using System;
using System.Collections.Generic;

namespace VegetableStorage
{
    public class Storage
    {
        private List<Container> _conts;
        public int Capacity { get; }
        public int Price { get; }
        public string Name { get; }

        public Storage(string name, int capacity, int price)
        {
            Name = name;
            Capacity = capacity;
            Price = price;
            _conts = new List<Container>();
        }

    }
}