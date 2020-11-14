using System;
using System.Collections.Generic;
using System.Linq;
using VegetableStorage.Commands;

namespace VegetableStorage
{
    public class CommandHandler
    {
        // Список инстансов доступных приложений (команд).
        private List<ICommand> _commands;
        public CommandHandler()
        {
            _commands = new List<ICommand>();
            _commands.Add(new CreateStorage("create"));
            _commands.Add(new StorageList("list"));
            _commands.Add(new Help("help", _commands));
        }
        
        /// <summary>
        /// Запускает выполнение команды.
        /// </summary>
        /// <param name="input">команда, введенная пользователем в консоль</param>
        /// <returns>результат выполнения команды</returns>
        public string Execute(string input)
        {
            var args = input.Split(' ');
            var appName = args[0];
            
            // Находим команду с заданным именем и выполняем ее.
            foreach (var app in _commands.Where(app => app.Name == appName))
            {
                return app.Run(args);
            }

            if (args[0] == Program.ExitCommand)
            {
                return "Bye!" + Environment.NewLine;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            return "Unknown command. Type \"help\" to get a list of available commands.";
        }
    }
}