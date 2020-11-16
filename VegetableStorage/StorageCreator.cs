using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using VegetableStorage.Entities;
using VegetableStorage.Exceptions;

namespace VegetableStorage
{
    /// <summary>
    /// Оператор создания склада
    /// и совершения действий над ним
    /// путем взаимодействия с пользователем
    /// через консоль.
    /// </summary>
    public class StorageCreator
    {
        private Storage _storage;
        private List<Operation> _actions;

        /// <summary>
        /// При создании объекта
        /// запрашиваются вместимость склада
        /// и цена хранения одного
        /// контейнера.
        /// </summary>
        public StorageCreator()
        {
            Console.WriteLine("Укажите максимальное число контейнеров от 1 до 200, которое будет вмещать склад:");

            int capacity;
            do
            {
                if (int.TryParse(Console.ReadLine(), out capacity) && 1 <= capacity && capacity <= 200) break;
                Console.WriteLine("Недопустимое значение, попробуйте еще раз.");
            } while (true);

            int price;

            Console.WriteLine("Укажите цену хранения одного контейнера в тугриках:");
            do
            {
                if (int.TryParse(Console.ReadLine(), out price) && 1 <= price) break;
                Console.WriteLine("Недопустимое значение, попробуйте еще раз.");
            } while (true);

            _storage = new Storage("default", capacity, price);
            Console.WriteLine("Склад успешно создан!");

            // Запрашиваем у пользователя действия.
            RequestActions();

            // Вывод результата.
            var writer = new StorageWriter(_storage);
            writer.WriteToConsole();
            Console.WriteLine();

            // Запрос на сохранение результата в файл.
            if (Program.RequestAgreement("Хотите записать результат в файл?"))
            {
                do
                {
                    Console.WriteLine("Введите полный путь к файлу:");
                    var path = Path.GetFullPath(Console.ReadLine() ?? string.Empty);
                    try
                    {
                        writer.WriteToFile(path);
                        Console.WriteLine("Информация успешно записана!");
                        break;
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Ошибка при чтении файла/директории.");
                        if (!Program.RequestAgreement("Повторить операцию?")) break;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("Ошибка доступа при чтении файла/директории.");
                        if (!Program.RequestAgreement("Повторить операцию?")) break;
                    }
                } while (true);
            }
            // Запрос на сохранение списка действий в файл.
            if (Program.RequestAgreement("Хотите записать список ваших действий в файл?"))
            {
                do
                {
                    Console.WriteLine("Введите полный путь к файлу:");
                    var path = Path.GetFullPath(Console.ReadLine() ?? string.Empty);
                    try
                    {
                        var sw  = new StreamWriter(path);
                        sw.WriteLine(JsonConvert.SerializeObject(_actions));
                        Console.WriteLine("Информация успешно записана!");
                        break;
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Ошибка при чтении файла/директории.");
                        if (!Program.RequestAgreement("Повторить операцию?")) break;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine("Ошибка доступа при чтении файла/директории.");
                        if (!Program.RequestAgreement("Повторить операцию?")) break;
                    }
                } while (true);
            }

        }

        /// <summary>
        /// Запрос последовательного ввода действий
        /// от пользователя.
        /// </summary>
        private void RequestActions()
        {
            Console.WriteLine("Вы можете совершать над складом два вида действий:");
            Console.WriteLine("-> add [id] - добавить новый контейнер. Опциально указывается идентификатор -");
            Console.WriteLine("произвольная строка длиной до 10 символов.");
            Console.WriteLine("-> remove <id> - удалить контейнер с индентификатором <id>.");
            Console.WriteLine("Чтобы завершить или прервать ввод действий, напишите exit.");

            var actionIterator = 1;
            do
            {
                Console.Write($"Действие #{actionIterator}> ");
                var userInput = Console.ReadLine()?.Trim().Split();
                var action = userInput?.Length > 1
                    ? new Operation(userInput?[0], userInput?[1])
                    : new Operation(userInput?[0], string.Empty);

                if (userInput?[0] == Program.ExitCommand)
                {
                    return;
                }

                _storage.ApplyAction(action);
                _actions.Add(action);
                actionIterator++;
                
            } while (true);
        }
    }
}