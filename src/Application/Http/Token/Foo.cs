using app.Common.Annotation;
using app.Domain.Entity.User;

namespace app.Application.Http.Token
{
    public class Foo
    {
        [Injected] 
        private IUserRepository UserRepository { get; set; }    
    }
}