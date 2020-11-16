using System;
using System.Collections.Generic;
using System.IO;
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

        // Счетчик идентификаторов для контейнеров, инкрементируется при
        // каждом добавлении контейнера.
        private int _idCounter;

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

            Console.WriteLine("Укажите цену хранения одного контейнера в тугриках (от 1 до 400):");
            do
            {
                if (int.TryParse(Console.ReadLine(), out price) && 1 <= price && price <= 400) break;
                Console.WriteLine("Недопустимое значение, попробуйте еще раз.");
            } while (true);

            _storage = new Storage("default", capacity, price);
            Console.WriteLine("Склад успешно создан!");
            _idCounter = 0;

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
                    : new Operation(userInput?[0]);

                if (userInput?[0] == Program.ExitCommand)
                {
                    return;
                }

                _storage.ApplyAction(action);
                actionIterator++;
                
            } while (true);
        }

        /// <summary>
        /// Запрос создания нового контейнера
        /// у пользователя.
        /// </summary>
        private void RequestAddOperation()
        {
            // Ввод числа ящиков.
            Console.WriteLine("Введите количество ящиков, которые хотите поместить в новый контейнер (от 1 до 20):");
            int amount;
            do
            {
                var input = Console.ReadLine();
                if (input == Program.ExitCommand) return;
                if (int.TryParse(input, out amount) && 1 <= amount && amount <= 20) break;
                Console.WriteLine("Недопустимое значение, попробуйте еще раз.");
            } while (true);

            // Ввод информации о ящиках.
            var container = new Container(_idCounter.ToString());
            Console.WriteLine($"В каждой из следующих {amount} строк введите через пробел по два целых числа -");
            Console.WriteLine("масса ящика в килограммах (от 1 до 100) и цена за килограмм в тугриках (от 1 до 50):");
            for (var i = 0; i < amount; i++)
            {
                Console.Write($"Ящик #{i + 1}> ");
                do
                {
                    var userInput = Console.ReadLine()?.Trim().Split();
                    if (userInput?[0] == Program.ExitCommand) return;
                    if (userInput?.Length == 2 && int.TryParse(userInput[0], out var weight) &&
                        int.TryParse(userInput[1], out var price) && 1 <= weight && 1 <= price && weight <= 100 &&
                        price <= 50)
                    {
                        try
                        {
                            container.AddBox(new Box(weight, price));
                        }
                        catch (BoxAddException)
                        {
                            Console.WriteLine("В контейнере не осталось места для такого ящика :(");
                        }

                        break;
                    }

                    Console.WriteLine("Неверный ввод, повторите еще раз.");
                } while (true);
            }

            // Проверка рентабельности хранения контейнера.
            if (container.TotalValue <= _storage.Price)
            {
                Console.WriteLine(
                    "К сожалению, хранение такого контейнера нерентабельно: ценность его содержимого " +
                    $"{container.TotalValue} тугриков, в то время как цена на хранение " +
                    $"составляет {_storage.Price} тугриков." +
                    "Данный контейнер помещен на склад не будет.");
                return;
            }

            // Добавление нового контейнера.
            _storage.AddContainer(container);
            Console.WriteLine($"Контейнер успешно добавлен на склад, ему присвоен идентификатор {_idCounter}");
            _idCounter++;
        }

        /// <summary>
        /// Запрос удаления контейнера.
        /// </summary>
        /// <param name="id">Идентификатор контейнера.</param>
        private void RequestRemoveOperation(string id)
        {
            try
            {
                _storage.RemoveContainerById(id);
                Console.WriteLine("Контейнер успешно удален.");
            }
            catch (ContainerNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}