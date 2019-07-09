using System;
using app.CQS.User.Output;
using app.Modules.ORM.Repositories.Implementations;
using app.Modules.ORM.Repositories.Interfaces;

namespace app.CQS.User.Query
{
    public class GetUserQuery : IExecutable
    {
        private IUserRepository UserRepository { get; }

        public GetUserQuery(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public UserOutput Execute(Guid userId)
        {
            return new UserOutput(UserRepository.FindUser(userId));
        }
    }
}