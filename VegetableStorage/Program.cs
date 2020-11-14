using System;
using System.Collections.Generic;

namespace VegetableStorage
{
    internal static class Program
    {
        public const string ExitCommand = "exit";

        public const int MaxNameLength = 24;
        
        public static List<Storage> Storages = new List<Storage>();
        
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==> Овощной склад (в перспективе - менеджер овощных складов) <==");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Введите help для получения справки.");
            Console.WriteLine("Введите create для создания склада.");

            // Основной цикл программы. Запрос ввода команды от пользователя.
            var handler = new CommandHandler();
            string userInput;
            do
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("> ");
                userInput = Console.ReadLine();
                // Отправляем то, что ввел пользователь, обработчику команд
                // и возвращаем результат.
                Console.WriteLine(handler.Execute(userInput));
            } while (userInput != ExitCommand);
        }
    }
}