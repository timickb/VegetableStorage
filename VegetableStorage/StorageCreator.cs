using System;
using System.Collections.Generic;
using VegetableStorage.Entities;

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

            RequestActions();
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
            Console.WriteLine("Чтобы завершить ввод действий, напишите exit.");

            var actionIterator = 1;
            do
            {
                Console.Write($"Действие #{actionIterator} > ");
                var userInput = Console.ReadLine()?.Trim().Split();
                if (userInput?[0] == "add")
                {
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
                    break;
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
            Console.WriteLine("Введите количество ящиков, которые хотите поместить в новый контейнер (от 1 до 20):");
            int amount;
            do
            {
                if (int.TryParse(Console.ReadLine(), out amount) && 1 <= amount && amount <= 20) break;
                Console.WriteLine("Недопустимое значение, попробуйте еще раз.");
            } while (true);
            
            var container = new Container();
            Console.WriteLine($"В каждой из следующих {amount} строк введите через пробел по два целых числа -");
            Console.WriteLine("масса ящика в килограммах и цена за килограмм в тугриках:");
            for (var i = 0; i < amount; i++)
            {
                var userInput = Console.ReadLine()?.Trim().Split();
            }
        }

        /// <summary>
        /// Запрос удаления контейнера.
        /// </summary>
        /// <param name="id">Идентификатор контейнера.</param>
        private void RequestRemoveOperation(string id)
        {
        }
    }
}