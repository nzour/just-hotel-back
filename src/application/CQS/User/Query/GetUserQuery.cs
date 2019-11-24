using System;
using Application.CQS.User.Output;
using Domain;
using Domain.Entities;

namespace Application.CQS.User.Query
{
    public class GetUserQuery
    {
        private IEntityRepository<UserEntity> UserRepository { get; }

        public GetUserQuery(IEntityRepository<UserEntity> userRepository)
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