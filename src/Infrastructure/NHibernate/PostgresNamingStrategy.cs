using NHibernate.Cfg;

namespace App.Infrastructure.NHibernate
{
    public class PostgresNamingStrategy : INamingStrategy
    {
        public string ClassToTableName(string className) => ToDoubleQuote(className);

        public string PropertyToColumnName(string propertyName) => ToDoubleQuote(propertyName);

        public string TableName(string tableName) => ToDoubleQuote(tableName);

        public string ColumnName(string columnName) => ToDoubleQuote(columnName);

        public string PropertyToTableName(string className, string propertyName) => ToDoubleQuote(propertyName);

        public string LogicalColumnName(string columnName, string propertyName)
        {
            return string.IsNullOrWhiteSpace(columnName)
                ? ToDoubleQuote(propertyName)
                : ToDoubleQuote(columnName);
        }

        private string ToDoubleQuote(string name) => $"\"{name.Replace("`", "")}\"";
    }
}