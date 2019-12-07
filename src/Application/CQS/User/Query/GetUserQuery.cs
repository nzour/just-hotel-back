using System;
using System.Threading.Tasks;
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

        public async Task<UserOutput> ExecuteAsync(Guid userId)
        {
            return new UserOutput(
                await UserRepository.GetAsync(userId)
            );
        }
    }
}