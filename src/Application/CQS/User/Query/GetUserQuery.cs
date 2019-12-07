using System;
using Application.CQS.User.Output;
using Domain;
using Domain.Entities;

namespace Application.CQS.User.Query
{
    public class GetUserQuery
    {
        private IRepository<UserEntity> UserRepository { get; }

        public GetUserQuery(IRepository<UserEntity> userRepository)
        {
            UserRepository = userRepository;
        }

        public UserOutput Execute(Guid userId)
        {
            return new UserOutput(
                UserRepository.Get(userId)
            );
        }
    }
}