using System;
using System.Linq;
using app.Domain.Entity;
using NHibernate;
using NHibernate.Cfg;

namespace app.Infrastructure.NHibernate
{
    public class NHibernateHelper    
    {
        private static ISessionFactory _sessionFactory;
 
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    // Регистрируем всех наследников AbstractEntity в Nhibernate
                    RegisterEntitiesRecursively(configuration, typeof(AbstractEntity));
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                
                return _sessionFactory;
            }
        }
 
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        /// <summary>
        /// Добавит всех наследников абстракного класса в конфигурацию Nhibernate (рекурсивно)
        /// </summary>
        /// <param name="configuration">Конфигурация Nhibernate</param>>
        /// <param name="class">Тип абстракного класса</param>
        private static void RegisterEntitiesRecursively(Configuration configuration, Type @class)
        {
            var assembly = typeof(Startup).Assembly;
            var entities = assembly.DefinedTypes.Where(type => type.IsSubclassOf(@class));

            foreach (var entity in entities)
            {
                if (entity.IsAbstract)
                {
                    RegisterEntitiesRecursively(configuration, entity);
                }
                else
                {
                    configuration.AddAssembly(entity.Assembly);
                }
            }
        }
    }
}