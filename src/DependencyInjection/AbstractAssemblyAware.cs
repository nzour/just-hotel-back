using System.Reflection;

namespace app.DependencyInjection
{
    /// <summary>
    /// Знает о текущей сборке, а также может вернуть сборку передаваемого класса.
    /// </summary>
    public abstract class AbstractAssemblyAware
    {
        protected static Assembly GetAssembly()
        {
            return typeof(Startup).Assembly;
        }

        protected static Assembly GetAssembly<T>()
        {
            return typeof(T).Assembly;
        }
    }
}