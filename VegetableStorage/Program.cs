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
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("===> Склад овощей <===");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
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
                        new StorageLoader();
                        break;
                    case "3":
                    case ExitCommand:
                        Console.WriteLine("Всего доброго!");
                        return;
                    default:
                        Console.WriteLine("Нет такой опции, попробуйте еще раз.");
                        continue;
                }
                
                if (RequestAgreement("Повторить работу программы?"))
                {
                    continue;
                }

                break;
            } while (true);
        }
        
        /// <summary>
        /// Вспомогательная функция.
        /// Запрашивает у пользователя некоторый выбор
        /// из "Да" и "Нет" и возвращает булево
        /// значение.
        /// </summary>
        /// <param name="message">Вопрос, который выводится пользователю.</param>
        /// <returns>true - пользователь согласен, false - не согласен.</returns>
        public static bool RequestAgreement(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{message} [Y/n] ");
            var reaction = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            return reaction == "Y" || reaction == "y" || reaction == "yes" || reaction == "Yes";
        }
        
        /// <summary>
        /// Незамедлительно завершает работу программы.
        /// </summary>
        public static void ImmediatelyStop()
        {
            
        }
    }
}