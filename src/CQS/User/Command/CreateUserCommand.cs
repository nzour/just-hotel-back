using app.CQS.User.Input;
using app.Modules.ORM.Repositories.Interfaces;

namespace app.CQS.User.Command
{
    public class CreateUserCommand : IExecutable
    {
        private IUserRepository UserRepository { get; }

        public CreateUserCommand(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public void Execute(UserInput input)
        {
            var user = new Modules.ORM.Entities.User();
            UserRepository.AddUser(user);
        }
    }
}