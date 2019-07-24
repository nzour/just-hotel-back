using System;

namespace app.Common
{
    /// <summary>
    /// Класс, знающий о данных для подключения к БД.
    /// Данные задаются в файле environment.json (по умолчанию)
    /// </summary>
    public struct DbAccessor
    {
        // Хост базы данных
        public static string Host { get; } = Environment.GetEnvironmentVariable("DB_HOST");
        
        // Имя базы данных
        public static string Database { get; } = Environment.GetEnvironmentVariable("DB_NAME");
        
        // Пользовать
        public static string User { get; } = Environment.GetEnvironmentVariable("DB_USER");
        
        // Пароль
        public static string Password { get; } = Environment.GetEnvironmentVariable("DB_PASSWORD");

        // Готовая страка для connecion к БД.
        public static string ConnectionString { get; } = $"Server={Host};Port=5432;Database={Database};User Id={User};Password={Password};";
    }
}