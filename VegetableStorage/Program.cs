/* Заметка для ревьювера
 * Шаблоны файлов для загрузки находятся
 * в директории ../examples,
 * Кроме того, на всякий случай
 * положил(а) сюда README.md, возможно,
 * будет полезно прочитать при возникновении
 * вопросов.
 */

using System;
using System.Collections.Generic;
using VegetableStorage.Entities;

namespace VegetableStorage
{
    /// <summary>
    /// Склад овощей.
    /// </summary>
    public static class Program
    {
        public const string ExitCommand = "exit";

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
                Console.WriteLine("2 - загрузить информацию из json файлов,");
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
    }
}