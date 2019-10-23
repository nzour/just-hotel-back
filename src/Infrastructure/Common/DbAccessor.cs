using System;

namespace App.Infrastructure.Common
{
    /// <summary>
    /// Класс, знающий о данных для подключения к БД.
    /// Данные задаются в файле environment.json (по умолчанию)
    /// </summary>
    public struct DbAccessor
    {
        // Хост базы данных
        public static string Host => Environment.GetEnvironmentVariable("DB_HOST") ?? "";
        
        // Имя базы данных
        public static string Database => Environment.GetEnvironmentVariable("DB_NAME") ?? "";
        
        // Пользовать
        public static string User => Environment.GetEnvironmentVariable("DB_USER") ?? "";
        
        // Пароль
        public static string Password => Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

        public static string Schema => Environment.GetEnvironmentVariable("DB_SCHEMA") ?? "";

        // Готовая строка для connecion к БД.
        public static string ConnectionString => $"Server={Host};Port=5432;Database={Database};User Id={User};Password={Password};";
    }
}