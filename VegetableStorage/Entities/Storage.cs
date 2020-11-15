using System;
using System.Collections.Generic;

namespace VegetableStorage.Entities
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
        /// Удаляет из хранилища контейнер по
        /// его хронологическому порядковому номеру.
        /// </summary>
        /// <param name="index">Порядковый номер.</param>
        /// <exception cref="ArgumentOutOfRangeException">Исключение выбрасывается, когда
        /// контейнера с указанным порядковым номером не существует.</exception>
        public void RemoveContainerByIndex(int index)
        {
            try
            {
                _conts.RemoveAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

    }
}