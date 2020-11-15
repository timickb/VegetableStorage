using System;
using VegetableStorage.Entities;

namespace VegetableStorage
{
    public class StorageWriter
    {
        private Storage _storage;
        
        public StorageWriter(Storage s)
        {
            _storage = s;
        }

        public void WriteToConsole()
        {
            Console.WriteLine("Информация о складе:");
            Console.WriteLine($"-> Число контейнеров: {_storage.Fullness}");
            Console.WriteLine($"-> Цена хранения контейнера: {_storage.Price}");
            Console.WriteLine($"-> Вместимость склада: {_storage.Capacity}");
            Console.WriteLine();
            Console.WriteLine("-> Контейнеры:");
            foreach (var cont in _storage.GetContainersList())
            {
                Console.WriteLine($"-> -> {cont.Id}:");
                Console.WriteLine($"-> -> Суммарная масса ящиков: {cont.TotalWeight}");
                Console.WriteLine($"-> -> Суммарная ценность ящиков: {cont.TotalValue}");
                Console.WriteLine($"-> -> Ящики:");
                foreach (var box in cont.GetBoxesList())
                {
                    Console.WriteLine($"-> -> -> {box.Weight} кг; {box.PriceForKilo} тугриков за кг.");
                }
            }
            
        }

        public void WriteToFile(string filename)
        {
            
        }
    }
}