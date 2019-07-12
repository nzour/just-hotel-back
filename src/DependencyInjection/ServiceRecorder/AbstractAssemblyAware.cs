using System.Reflection;

namespace app.DependencyInjection.ServiceRecorder
{
    public class AbstractAssemblyAware
    {
        protected Assembly GetAssembly()
        {
            return typeof(Startup).Assembly;
        }
    }
}