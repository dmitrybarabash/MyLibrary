using System.IO;
using Microsoft.Extensions.Configuration;

namespace MyLibrary.Common.Options;

public static class GlobalOptions
{
    /// <summary>
    /// Возвращает строку соединения с БД.
    /// </summary>
    public static string ConnectionString
    {
        get
        {
            // Если существует внешний файл appsettings.json и в нем указана строка соединения,
            // берем ее оттуда. Если не указана, то берем дефолтную строку соединения.

            // Дефолтная строка соединения
            string defaultConnectionString = "Data Source=(local);Initial Catalog=MyLibrary;Integrated Security=True";

            // Строим конфигурацию из файла appsettings.json и из секретов пользователя.
            // Оба источника необязательны: если файла нет, исключение не выбрасывается.
            // Значения из секретов перекрывают значения из appsettings.json.
            IConfiguration configuration =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddUserSecrets(typeof(GlobalOptions).Assembly)
                    .Build();

            // Получаем строку подключения из параметра конфигурации ConnectionStrings:Database или null,
            // если этого параметра нет ни в appsettings.json, ни в секретах
            string connectionString = configuration.GetConnectionString("Database");

            return !string.IsNullOrEmpty(connectionString) ? connectionString : defaultConnectionString;
        }
    }
}
