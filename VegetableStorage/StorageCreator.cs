using System;
using System.Collections.Generic;
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
            var writer = new StorageWriter(_storage);
            writer.WriteToConsole();
        }

        /// <summary>
        /// Запрос последовательного ввода действий
        /// от пользователя.
        /// </summary>
        private void RequestActions()
        {
            Console.WriteLine("Вы можете совершать над складом два вида действий:");
            Console.WriteLine("-> add - добавить новый контейнер.");
            Console.WriteLine("-> remove <id> - удалить контейнер с индентификатором <id>.");
            Console.WriteLine("Чтобы завершить или прервать ввод действий, напишите exit.");

            var actionIterator = 1;
            do
            {
                Console.Write($"Действие #{actionIterator}> ");
                var userInput = Console.ReadLine()?.Trim().Split();
                if (userInput?[0] == "add")
                {
                    if (_storage.Fullness >= _storage.Capacity)
                    {
                        Console.WriteLine("(!) Склад уже содержит максимальное число контейнеров.");
                        Console.WriteLine("При добавлении нового контейнера будет удален самый старый.");
                        Console.WriteLine();
                    }
                    RequestAddOperation();
                    actionIterator++;
                }
                else if (userInput?[0] == "remove" && userInput.Length >= 2)
                {
                    RequestRemoveOperation(userInput[1]);
                    actionIterator++;
                }
                else if (userInput?[0] == "exit")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Неизвестная команда или неверные аргументы.");
                }
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
                if (input.Equals("exit")) return;
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
                    if (userInput.Equals("exit")) return;
                    int weight, price;
                    if (userInput?.Length == 2 && int.TryParse(userInput[0], out weight) &&
                        int.TryParse(userInput[1], out price) && 1 <= weight && 1 <= price && weight <= 100 &&
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