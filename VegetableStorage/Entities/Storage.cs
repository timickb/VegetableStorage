using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VegetableStorage.Exceptions;

namespace VegetableStorage.Entities
{
    public class Storage
    {
        public string Name { get; }
        public int Capacity { get; }
        public int Price { get; }
        public int Fullness => Containers.Count;
        public List<Container> Containers { get; set; }

        public Storage(string name, int capacity, int price)
        {
            Name = name;
            Capacity = capacity;
            Price = price;
            Containers = new List<Container>();
        }
        
        /// <summary>
        /// Добавляет в хранилище новый контейнер.
        /// В случае, если он не вмещается,
        /// удаляет самый ранний из добавленных.
        /// </summary>
        /// <param name="c">Контейнер</param>
        public void AddContainer(Container c)
        {
            if (Containers.Count >= Capacity)
            {
                Containers.RemoveAt(0);
            }
            Containers.Add(c);
        }
        
        /// <summary>
        /// Удаляет контейнер по его
        /// идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <exception cref="ContainerNotFoundException">Исключение выбрасывается,
        /// когда контейнера с данным идентификатром не существует.</exception>
        public void RemoveContainerById(string id)
        {
            foreach (var cont in Containers.Where(cont => cont.Id == id))
            {
                Containers.Remove(cont);
                return;
            }
            throw new ContainerNotFoundException();
        }

        public void ApplyAction(Action act)
        {
            if (act.Name == "add")
            {
                
            }
        }

        public override string ToString()
        {
            var sep = Environment.NewLine;
            var result = $"Информация о складе {Name}:" + sep;
            result += $"-> Число контейнеров: {Fullness}" + sep;
            result += $"-> Цена хранения контейнера: {Price}" + sep;
            result += $"-> Вместимость склада: {Capacity}" + sep;
            result += "-> Контейнеры:" + sep;
            foreach (var cont in Containers)
            {
                result += $"      {cont.Id}:" + sep;
                result += $"      Суммарная масса ящиков: {cont.TotalWeight}" + sep;
                result += $"      Суммарная ценность ящиков: {cont.TotalValue}" + sep;
                result += $"      Ящики:" + sep;
                foreach (var box in cont.Boxes)
                {
                    result += $"         {box.Weight} кг; {box.PriceForKilo} тугриков за кг." + sep;
                }
            }

            return result;
        }
    }
}