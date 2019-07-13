using app.Common.Annotation;
using NHibernate;

namespace app.Infrastructure.NHibernate.Repository
{
    /// <summary>
    /// Класс, который информаицю о текущей сессии с БД.
    /// Полезно для репозиториев. 
    /// </summary>
    public class AbstractSessionAware
    {
        [Injected] 
        protected ISession Session { get; set; }
    }
}