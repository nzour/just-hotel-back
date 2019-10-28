using Application.CQS.User.Output;
using Common.Extensions;
using Common.Util;
using Domain.UserEntity;

namespace Application.CQS.User.Query
{
    public class GetAllUsersQuery
    {
        private IUserRepository UserRepository { get; }

        public GetAllUsersQuery(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public PaginatedData<UserOutput> Execute(Pagination pagination)
        {
            return UserRepository
                .FindAllUsers()
                .Paginate<UserOutput>(pagination);
        }
    }
}