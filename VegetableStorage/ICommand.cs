namespace VegetableStorage
{
    
    /// <summary>
    /// Интерфейс, описывающий взаимодействие
    /// обработчика команд с каждой конкретной
    /// командой.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Название команды для ее
        /// вызова через консоль.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Текст, отображаемый в консоли
        /// при неверном вводе аргументов
        /// команды.
        /// </summary>
        string Usage { get; }
        
        /// <summary>
        /// Описание данной команды.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Запускает выполнение данной команды.
        /// </summary>
        /// <param name="args">Аргументы команды.</param>
        /// <returns>Строка - результат выполнения команды.</returns>
        string Run(string[] args);
    }
}