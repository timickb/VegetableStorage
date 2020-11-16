using System;
using System.Collections.Generic;

namespace VegetableStorage
{
    
    /// <summary>
    /// Класс предоставляет статические сущности
    /// для получения строковых констант в зависимости
    /// от региональной настройки программы.
    /// </summary>
    public static class Lang
    {
        public static string CurrentLang = "Russian";

        public static Dictionary<string, Dictionary<string, string>> Text =
            new Dictionary<string, Dictionary<string, string>>()
            {
                {"Russian", Russian},
                {"English", English}
            };

        private static Dictionary<string, string> Russian = new Dictionary<string, string>()
        {
            {"title", "===> Склад овощей <==="},
            {"chooseOption", "Выберите опцию: "},
            {"option1", "1 - ввести параметры склада в консоли,"},
            {"option2", "2 - загрузить информацию о складе из json файлов,"},
            {"option3", "3 - отстаньте, я хочу выйти"}
        };
        private static Dictionary<string, string> English = new Dictionary<string, string>()
        {
            {"title", "===> Vegetables Storage <==="},
            {"chooseOption", "Choose option: "},
            {"option1", "1 - input storage parameters from the console,"},
            {"option2", "2 - load storage parameters from JSON files,"},
            {"option3", "3 - I want to exit."}
        };
    }
    
}