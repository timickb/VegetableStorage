using System;
using System.IO;
using Newtonsoft.Json;
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
            Console.WriteLine("-> Контейнеры:");
            foreach (var cont in _storage.Containers)
            {
                Console.WriteLine($"      {cont.Id}:");
                Console.WriteLine($"      Суммарная масса ящиков: {cont.TotalWeight}");
                Console.WriteLine($"      Суммарная ценность ящиков: {cont.TotalValue}");
                Console.WriteLine($"      Ящики:");
                foreach (var box in cont.Boxes)
                {
                    Console.WriteLine($"         {box.Weight} кг; {box.PriceForKilo} тугриков за кг.");
                }
            }
            
        }

        public void WriteToFile(string path)
        {
            try
            {
                var sw = new StreamWriter(path, false, System.Text.Encoding.UTF8);
                sw.WriteLine(JsonConvert.SerializeObject(_storage));
                sw.Flush();
                sw.Close();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Невозможно записать информацию в заданный файл.");
            }
            catch (IOException)
            {
               Console.WriteLine("Невозможно записать информацию в заданный файл."); 
            }
        }
    }
}