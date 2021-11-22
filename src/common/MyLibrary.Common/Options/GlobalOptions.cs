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

            // Создаем объект построения конфигурации, которую будем брать из файла appsettings.json
            var configurationBuilder =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
            // Пытаемся создать собственно объект конфигурации, если такой файл существует
            IConfiguration configuration = null;
            try
            {
                configuration = configurationBuilder.Build();
            }
            catch (FileNotFoundException)
            {
            }

            // Получаем строку подключения из параметра конфигурации DefaultConnection  или null,
            // если файла appsettings.json или параметра DefaultConnection в нем не существует
            string connectionString = configuration?.GetConnectionString("DefaultConnection");

            return !string.IsNullOrEmpty(connectionString) ? connectionString : defaultConnectionString;
        }
    }
}
