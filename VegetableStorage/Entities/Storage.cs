using System;
using System.Collections.Generic;
using System.Linq;
using VegetableStorage.Exceptions;

namespace VegetableStorage.Entities
{
    public class Storage
    {
        private List<Container> _conts;
        
        public int Capacity { get; }
        public int Price { get; }
        public int Fullness => _conts.Count;
        public string Name { get; }

        public Storage(string name, int capacity, int price)
        {
            Name = name;
            Capacity = capacity;
            Price = price;
            _conts = new List<Container>();
        }
        
        /// <summary>
        /// Добавляет в хранилище новый контейнер.
        /// В случае, если он не вмещается,
        /// удаляет самый ранний из добавленных.
        /// </summary>
        /// <param name="c">Контейнер</param>
        public void AddContainer(Container c)
        {
            if (_conts.Count >= Capacity)
            {
                _conts.RemoveAt(0);
            }
            _conts.Add(c);
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
            foreach (var cont in _conts.Where(cont => cont.Id == id))
            {
                _conts.Remove(cont);
                return;
            }
            throw new ContainerNotFoundException();
        }

    }
}