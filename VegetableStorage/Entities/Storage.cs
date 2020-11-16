using System;
using System.Collections.Generic;
using System.Linq;
using VegetableStorage.Exceptions;

namespace VegetableStorage.Entities
{
    public class Storage
    {
        public string Name { get; }
        public int Capacity { get; }
        public int Price { get; }
        public int Fullness => Containers.Count;
        public List<Container> Containers { get; }

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

    }
}