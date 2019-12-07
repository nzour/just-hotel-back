using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernateConfiguration = NHibernate.Cfg.Configuration;

namespace Infrastructure.NHibernate
{
    public static class NHibernateSessionFactory
    {
        public static ISessionFactory CreateSessionFactory(string connectionString)
        {
            var configuration = new NHibernateConfiguration().SetNamingStrategy(new PostgresNamingStrategy());

            return Fluently.Configure(configuration)
                .Database(PostgreSQLConfiguration.PostgreSQL82.ConnectionString(connectionString))
                .Mappings(ConfigureMappings)
                .BuildSessionFactory();
        }

        private static void ConfigureMappings(MappingConfiguration mappings)
        {
            mappings.FluentMappings.AddFromAssembly(typeof(NHibernateSessionFactory).Assembly);
        }
    }
}