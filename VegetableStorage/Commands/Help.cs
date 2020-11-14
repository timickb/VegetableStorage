using System;
using System.Collections.Generic;

namespace VegetableStorage.Commands
{
    public class Help : ICommand
    {
        public string Name { get; }
        public string Usage { get; }
        public string Description { get; }

        private List<ICommand> _commands;

        public Help(string name, List<ICommand> commands)
        {
            Name = name;
            Usage = "help";
            Description = "Выводит список доступных команд и инструкции к их использованию.";
            _commands = commands;
        }
        public string Run(string[] args)
        {
            var sep = Environment.NewLine;
            
            var text = "Для того, чтобы работать с программой, нужно создать склад." + sep + sep;

            foreach (var command in _commands)
            {
                text += $"-> {command.Name}" + sep;
                text += $"Использование: {command.Usage}" + sep;
                text += command.Description + sep + sep;
            }

            return text;
        }
    }
}