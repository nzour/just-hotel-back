using Ninject.Planning.Targets;

namespace app.Common.Attribute
{
    /// <summary>
    /// Помечаются методы контроллера, либо сам контроллер. Если нет необходимости в endpoint'e проверять токен.
    /// </summary>
    public class AnonymousAttribute : System.Attribute
    {
    }
}