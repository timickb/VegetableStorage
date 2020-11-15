using System;
using System.Collections.Generic;
using System.Linq;
using VegetableStorage.Exceptions;

namespace VegetableStorage
{
    public class Container
    {
        private readonly int _maxWeight;
        private readonly double _integrity;
        private List<Box> _boxes;
        
        public string Id { get; }
        public int MaxWeight => _maxWeight;
        public int TotalWeight => _boxes.Sum(box => box.Weight);
        public double TotalValue => _boxes.Sum(box => box.TotalPrice);

        public Container(string id)
        {
            Id = id;
            var rnd = new Random();
            // Максимальная масса хранимых ящиков - случайное число из [50, 1000].
            _maxWeight = rnd.Next(50, 1001);
            _boxes = new List<Box>();

            // Рассчитываю степень целостности контейнера (1 - <степень_повреждения>).
            _integrity = 1 - (rnd.NextDouble() * 0.5);
        }

        public void AddBox(Box box)
        {
            if (TotalWeight + box.Weight > MaxWeight) throw new BoxAddException();

            // Умножаем стоимость ящика на коэффициент целостности данного контейнера.
            box.PriceForKilo *= _integrity;
            _boxes.Add(box);
        }
    }
}