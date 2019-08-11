using System;
using app.Application.CQS.User.Output;
using app.Domain.Entity.User;

namespace app.Application.CQS.User.Query
{
    public class GetUserQuery
    {
        public IUserRepository UserRepository { get; }

        public GetUserQuery(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public UserOutput Execute(Guid userId)
        {
            return new UserOutput(UserRepository.GetUser(userId));
        }
    }
}