using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using Newtonsoft.Json;
using VegetableStorage.Entities;
using JsonException = Newtonsoft.Json.JsonException;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace VegetableStorage
{
    public class StorageLoader
    {
        private Storage _storage;
        private List<Container> _containers;
        private JsonSerializerSettings settings;

        public StorageLoader()
        {
            // Сообщаем компилятору, что отсутствие некоторых свойств
            // при десериализации json нужно считать за ошибку.
            settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Error;
            
            RequestStorageDescription();
            RequestContainersList();
            RequestActionsList();

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

        private void RequestStorageDescription()
        {
            Storage storage;
            do
            {
                Console.WriteLine("Введите полный путь к JSON файлу с описанием склада:");
                var path = Path.GetFullPath(Console.ReadLine() ?? string.Empty);
                try
                {
                    var sr = new StreamReader(path);
                    storage = JsonConvert.DeserializeObject<Storage>(sr.ReadToEnd(), settings);
                    if (storage?.Containers.Count > 0)
                    {
                        Error("В файле содержится лишняя информация - список контейнеров. Попробуйте другой файл.");
                        continue;
                    }

                    break;
                }
                catch (IOException)
                {
                    Error("Ошибка чтения файла.");
                }
                catch (UnauthorizedAccessException)
                {
                    Error("Ошибка доступа к файлу.");
                }
                catch (JsonReaderException)
                {
                    Error("Файл не соответствует формату. Смотрите образец в examples/storage.json");
                }
                catch (JsonSerializationException)
                {
                    Error("Файл не соответствует формату. Смотрите образец в examples/storage.json");
                }
                catch (JsonException)
                {
                    Error("Файл не соответствует формату. Смотрите образец в examples/storage.json");
                }
            } while (true);

            _storage = storage;
            Console.WriteLine("Информация о складе успешно загружена.");
        }

        private void RequestContainersList()
        {
            List<Container> list;
            do
            {
                Console.WriteLine("Введите полный путь к JSON файлу со списком контейнеров:");
                var path = Path.GetFullPath(Console.ReadLine() ?? string.Empty);
                try
                {
                    var sr = new StreamReader(path);
                    list = JsonConvert.DeserializeObject<List<Container>>(sr.ReadToEnd(), settings);
                    break;
                }
                catch (IOException)
                {
                    Console.WriteLine("Ошибка чтения файла.");
                    // Если пользователю надоест вводить неправильный путь - у него есть возможность
                    // прервать выполнение программы.
                    if (!Program.RequestAgreement("Повторить операцию?"))
                    {
                        Environment.Exit(1);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Error("Ошибка доступа к файлу.");
                }
                catch (JsonReaderException)
                {
                    Error("Файл не соответствует формату. Смотрите образец в examples/containers.json");
                }
                catch (JsonSerializationException)
                {
                    Error("Файл не соответствует формату. Смотрите образец в examples/containers.json");
                }
                catch (JsonException)
                {
                    Error("Файл не соответствует формату. Смотрите образец в examples/containers.json");
                }
            } while (true);

            // Закидываем контейнеры на склад.
            _containers = list;
        }

        private void RequestActionsList()
        {
            List<Operation> list;
            do
            {
                Console.WriteLine("Введите полный путь к JSON файлу со списком действий:");
                var path = Path.GetFullPath(Console.ReadLine() ?? string.Empty);
                try
                {
                    var sr = new StreamReader(path);
                    list = JsonConvert.DeserializeObject<List<Operation>>(sr.ReadToEnd(), settings);
                    break;
                }
                catch (IOException)
                {
                    Error("Ошибка чтения файла.");
                }
                catch (UnauthorizedAccessException)
                {
                    Error("Ошибка доступа к файлу.");
                }
                catch (JsonReaderException)
                {
                    Error("Файл не соответствует формату. Смотрите образец в examples/actions.json");
                }
                catch (JsonSerializationException)
                {
                    Error("Файл не соответствует формату. Смотрите образец в examples/actions.json");
                }
                catch (JsonException)
                {
                    Error("Файл не соответствует формату. Смотрите образец в examples/actions.json");
                }
            } while (true);

            // Поочередно применяем каждое действие ко складу.
            foreach (var action in list.Where(action => !_storage.ApplyAction(action, _containers)))
            {
                Console.WriteLine($"(!) Контейнер {action.Argument} не может быть добавлен на склад.");
            }
        }

        /// <summary>
        /// Сообщение об ошибке с предложением
        /// повторить ввод данных>.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        private void Error(string message)
        {
            Console.WriteLine(message);
            // Если пользователю надоест вводить неправильный путь - у него есть возможность
            // прервать выполнение программы.
            if (!Program.RequestAgreement("Повторить операцию?"))
            {
                Environment.Exit(18);
            }
        }
    }
}