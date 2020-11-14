using System;
using System.Collections.Generic;
using System.Linq;

namespace VegetableStorage.Commands
{
    public class StorageList : ICommand
    {
        public string Name { get; }
        public string Usage { get; }
        public string Description { get; }

        public StorageList(string name)
        {
            Name = name;
            Usage = "list";
            Description =
                "Выводит список существующих в системе хранилищ (в данной реализации список может быть либо пустым, либо содержать только 1 элемент).";
        }

        public string Run(string[] args)
        {
            var names = Program.Storages.Select(storage => storage.Name).ToList();
            return $"В системе {Program.Storages.Count} хранилиц: " + Environment.NewLine +
                   String.Join(Environment.NewLine, names);
        }
    }
}