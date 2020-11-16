using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using Newtonsoft.Json;
using VegetableStorage.Entities;

namespace VegetableStorage
{
    public class StorageLoader
    {
        private Storage _storage;
        public StorageLoader()
        {
            RequestStorageDescription();
            RequestContainersList();
            RequestActionsList();
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
                    storage = JsonConvert.DeserializeObject<Storage>(sr.ReadToEnd());
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
                    Console.WriteLine("Ошибка доступа к файлу.");
                    // Если пользователю надоест вводить неправильный путь - у него есть возможность
                    // прервать выполнение программы.
                    if (!Program.RequestAgreement("Повторить операцию?"))
                    {
                        Environment.Exit(1);
                    }
                }
                catch (JsonReaderException)
                {
                    Console.WriteLine("Файл не соответствует формату. Смотрите образец в examples/storage.json");
                    // Если пользователю надоест вводить неправильный путь - у него есть возможность
                    // прервать выполнение программы.
                    if (!Program.RequestAgreement("Повторить операцию?"))
                    {
                        Environment.Exit(1);
                    }
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
                    list = JsonConvert.DeserializeObject<List<Container>>(sr.ReadToEnd());
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
                    Console.WriteLine("Ошибка доступа к файлу.");
                    // Если пользователю надоест вводить неправильный путь - у него есть возможность
                    // прервать выполнение программы.
                    if (!Program.RequestAgreement("Повторить операцию?"))
                    {
                        Environment.Exit(1);
                    }
                }
                catch (JsonReaderException)
                {
                    Console.WriteLine("Файл не соответствует формату. Смотрите образец в examples/containers.json");
                    // Если пользователю надоест вводить неправильный путь - у него есть возможность
                    // прервать выполнение программы.
                    if (!Program.RequestAgreement("Повторить операцию?"))
                    {
                        Environment.Exit(1);
                    }
                }
            } while (true);
            // Закидываем контейнеры на склад.
            _storage.Containers = list;
        }

        private void RequestActionsList()
        {
            
        }
    }
}