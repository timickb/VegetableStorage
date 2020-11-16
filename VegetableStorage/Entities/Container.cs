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
        public List<Box> Boxes { get; }

        public string Id { get; }
        public int MaxWeight => _maxWeight;
        public int TotalWeight => Boxes.Sum(box => box.Weight);
        public double TotalValue => Boxes.Sum(box => box.TotalPrice);

        public Container(string id)
        {
            Id = id;
            var rnd = new Random();
            // Максимальная масса хранимых ящиков - случайное число из [50, 1000].
            _maxWeight = rnd.Next(50, 1001);
            Boxes = new List<Box>();

            // Рассчитываю степень целостности контейнера (1 - <степень_повреждения>).
            _integrity = 1 - (rnd.NextDouble() * 0.5);
        }

        public void AddBox(Box box)
        {
            if (TotalWeight + box.Weight > MaxWeight) throw new BoxAddException();

            // Умножаем стоимость ящика на коэффициент целостности данного контейнера.
            box.PriceForKilo *= _integrity;
            Boxes.Add(box);
        }

        /// <summary>
        /// Строковое представление контейнера.
        /// </summary>
        /// <returns>Строковое представление контейнера.</returns>
        public override string ToString()
        {
            var sep = Environment.NewLine;
            var result = "-> Контейнер {Id}:" + sep;
            result += $"-> Макисмальная суммарная масса: {MaxWeight}" + sep;
            result += $"-> Суммарная масса ящиков: {TotalWeight} кг." + sep;
            result += $"-> Суммарная ценность ящиков: {TotalValue} тугриков" + sep;
            result += "-> Ящики: " + sep;
            return Boxes.Aggregate(result,
                (current, box) => current + ($"      {box.Weight} кг; {box.PriceForKilo} тугриков за кг." + sep));
        }
    }
}