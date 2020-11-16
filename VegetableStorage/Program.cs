/* Заметка для ревьювера
 * Шаблоны файлов для загрузки находятся
 * в директории ../examples,
 * Кроме того, на всякий случай
 * положил(а) сюда README.md, возможно,
 * будет полезно прочитать при возникновении
 * вопросов.
 * Доп. функционал - возможность записать в файл
 * не только состояние склада, но и список действий,
 * совершенных пользователем над этим складом.
 */

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using VegetableStorage.Entities;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
            // Недопиленный функционал.
            if (args.Length > 1)
            {
                switch (args[1])
                {
                    case "Russian":
                        Lang.CurrentLang = "Russian";
                        break;
                    case "English":
                        Lang.CurrentLang = "English";
                        break;
                }
            }
            
            // Основной цикл программы.
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