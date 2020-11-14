namespace VegetableStorage.Commands
{
    public class CreateStorage : ICommand
    {
        public CreateStorage(string name)
        {
            Name = name;
            Usage = "create <storage_name> <storage_capacity> <storage_price>";
            Description =
                "Создает новое хранилище с именем storage_name, максимальной вместимостью storage_capacity и ценой за хранение одного контейнера storage_price.";
        }

        public string Name { get; }
        public string Usage { get; }
        public string Description { get; }

        public string Run(string[] args)
        {
            if (args.Length < 4)
            {
                return $"Использование: {Usage}";
            }

            if (args[1].Trim().Length > Program.MaxNameLength)
            {
                return $"Имя хранилища не должно превышать {Program.MaxNameLength} символов.";
            }

            var name = args[1].Trim();

            int capacity, price;
            if (!int.TryParse(args[2], out capacity) || capacity <= 0)
            {
                return $"Вместимость хранилища должна быть целым положительным числом.";
            }

            if (!int.TryParse(args[3], out price) || price <= 0)
            {
                return $"Стоимость хранения контейнера должна быть целым положительным числом.";
            }

            if (Program.Storages.Count > 0)
            {
                return $"Хранилище уже существует.";
            }

            Program.Storages.Add(new Storage(name, capacity, price));

            return "Хранилище успешно создано!";
        }
    }
}