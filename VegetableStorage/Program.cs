using System;
using System.Collections.Generic;
using VegetableStorage.Entities;

namespace VegetableStorage
{
    
    /// <summary>
    /// Склад овощей.
    /// </summary>
    internal static class Program
    {
        public const string ExitCommand = "exit";

        public const int MaxNameLength = 24;
        
        public static readonly List<Storage> Storages = new List<Storage>();
        
        public static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("===> Склад овощей <===");
                Console.WriteLine();
                Console.WriteLine("Выберите опцию:");
                Console.WriteLine("1 - ввести параметры склада в консоли,");
                Console.WriteLine("2 - загрузить информацию из json файла,");
                Console.WriteLine("3 - отстаньте, хочу выйти.");
                Console.Write("> ");
                switch (Console.ReadLine())
                {
                    case "1":
                        new StorageCreator();
                        break;
                    case "2":
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine("Всего доброго!");
                        return;
                    default:
                        Console.WriteLine("Нет такой опции, попробуйте еще раз.");
                        continue;
                }

                Console.Write("Повторить работу программы? [Y/n] ");
                var userInput = Console.ReadLine();
                if (userInput == "Y" || userInput == "y" || userInput == "Yes")
                {
                    continue;
                }
                break;
            } while (true);
        }
    }
}