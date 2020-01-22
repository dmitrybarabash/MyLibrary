using System.Configuration;

namespace MyLibrary.Common.Options
{
    public static class Options
    {
        /// <summary>
        /// Возвращает строку соединения с БД.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                // Если во внешнем config-файле MyLibrary.exe.config указана строка соединения,
                // берем ее оттуда. Если не указана, то берем ее из вкомпилированного App.config.
                // Если и там ее нет, то берем дефолтную строку соединения. (На самом деле она там
                // есть, смотри App.config в exe-проектах. MyLibrary.exe.config создается при
                // компиляции автоматически на основе App.config.)

                // Дефолтная строка соединения
                string defaultConnectionString = "Data Source=(local);Initial Catalog=MyLibrary;Integrated Security=True";

                // Классы ConfigurationManager и ConnectionStringSettings требуют
                // явного подключения сборки System.Configuration
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["MyLibrary"];

                return settings != null ? settings.ConnectionString : defaultConnectionString;
            }
        }
    }
}