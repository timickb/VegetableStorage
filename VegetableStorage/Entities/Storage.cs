using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VegetableStorage.Exceptions;

namespace VegetableStorage.Entities
{
    public class Storage
    {
        public string Name { get; }
        public int Capacity { get; }
        public int Price { get; }
        public int Fullness => Containers.Count;
        public List<Container> Containers { get; set; }

        // Счетчик идентификаторов для создания новых контейнеров.
        private int _idCounter;

        public Storage(string name, int capacity, int price)
        {
            Name = name;
            Capacity = capacity;
            Price = price;
            Containers = new List<Container>();
            _idCounter = 0;
        }

        /// <summary>
        /// Добавляет в хранилище новый контейнер.
        /// В случае, если он не вмещается,
        /// удаляет самый ранний из добавленных.
        /// </summary>
        /// <param name="c">Контейнер</param>
        public void AddContainer(Container c)
        {
            if (Containers.Count >= Capacity)
            {
                Containers.RemoveAt(0);
            }

            Containers.Add(c);
        }

        /// <summary>
        /// Удаляет контейнер по его
        /// идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <exception cref="ContainerNotFoundException">Исключение выбрасывается,
        /// когда контейнера с данным идентификатром не существует.</exception>
        public void RemoveContainerById(string id)
        {
            foreach (var cont in Containers.Where(cont => cont.Id == id))
            {
                Containers.Remove(cont);
                return;
            }

            throw new ContainerNotFoundException();
        }

        public void ApplyAction(Action act)
        {
            if (act.Name == "add")
            {
                if (Fullness >= Capacity)
                {
                    Console.WriteLine("(!) Склад уже содержит максимальное число контейнеров.");
                    Console.WriteLine("При добавлении нового контейнера будет удален самый старый.");
                    Console.WriteLine();
                }
                // Ввод числа ящиков.
                Console.WriteLine(
                    "Введите количество ящиков, которые хотите поместить в новый контейнер (от 1 до 20):");
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
                Console.WriteLine(
                    "масса ящика в килограммах (от 1 до 100) и цена за килограмм в тугриках (от 1 до 50):");
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
                if (container.TotalValue <= Price)
                {
                    Console.WriteLine(
                        "К сожалению, хранение такого контейнера нерентабельно: ценность его содержимого " +
                        $"{container.TotalValue} тугриков, в то время как цена на хранение " +
                        $"составляет {Price} тугриков." +
                        "Данный контейнер помещен на склад не будет.");
                    return;
                }

                // Добавление нового контейнера.
                AddContainer(container);
                Console.WriteLine($"Контейнер успешно добавлен на склад, ему присвоен идентификатор {_idCounter}");
                _idCounter++;
            }

            else if (act.Name == "remove")
            {
                try
                {
                    RemoveContainerById(act.Argument);
                    Console.WriteLine("Контейнер успешно удален.");
                }
                catch (ContainerNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Такой команды не существует.");
            }
        }

        public override string ToString()
        {
            var sep = Environment.NewLine;
            var result = $"Информация о складе {Name}:" + sep;
            result += $"-> Число контейнеров: {Fullness}" + sep;
            result += $"-> Цена хранения контейнера: {Price}" + sep;
            result += $"-> Вместимость склада: {Capacity}" + sep;
            result += "-> Контейнеры:" + sep;
            foreach (var cont in Containers)
            {
                result += $"      {cont.Id}:" + sep;
                result += $"      Суммарная масса ящиков: {cont.TotalWeight}" + sep;
                result += $"      Суммарная ценность ящиков: {cont.TotalValue}" + sep;
                result += $"      Ящики:" + sep;
                foreach (var box in cont.Boxes)
                {
                    result += $"         {box.Weight} кг; {box.PriceForKilo} тугриков за кг." + sep;
                }
            }

            return result;
        }
    }
}