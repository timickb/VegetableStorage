using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using VegetableStorage.Exceptions;

namespace VegetableStorage
{
    public class Container
    {
        private readonly double _integrity;
        
        [JsonProperty("Boxes")]
        public List<Box> Boxes { get; }

        [JsonProperty("Id")]
        public string Id { get; }
        
        [JsonProperty("MaxWeight")]
        public int MaxWeight { get; }
        public int TotalWeight => Boxes.Sum(box => box.Weight);
        public double TotalValue => Boxes.Sum(box => box.TotalPrice * _integrity);
        
        [JsonConstructor]
        public Container(string id, int maxWeight, List<Box> boxes)
        {
            Id = id;
            MaxWeight = maxWeight;
            Boxes = boxes;
        }
        
        public Container(string id)
        {
            Id = id;
            var rnd = new Random();
            // Максимальная масса хранимых ящиков - случайное число из [50, 1000].
            MaxWeight = rnd.Next(50, 1001);
            Boxes = new List<Box>();

            // Рассчитываю степень целостности контейнера (1 - <степень_повреждения>).
            _integrity = 1 - (rnd.NextDouble() * 0.5);
            Console.WriteLine($"(!) Контейнер {Id} поврежден, его целостность составляет {_integrity}");
        }

        public void AddBox(Box box)
        {
            if (TotalWeight + box.Weight > MaxWeight) throw new BoxAddException();
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
            result += $"-> Суммарная ценность ящиков с учетом повреждения: {TotalValue} тугриков" + sep;
            result += "-> Ящики: " + sep;
            return Boxes.Aggregate(result,
                (current, box) => current + ($"      {box.Weight} кг; {box.PriceForKilo} тугриков за кг." + sep));
        }
    }
}