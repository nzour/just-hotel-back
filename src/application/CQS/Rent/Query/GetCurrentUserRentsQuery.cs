using System.Linq;
using Application.CQS.Rent.Output;
using Common.Extensions;
using Common.Util;
using Domain.User;

namespace Application.CQS.Rent.Query
{
    public class GetCurrentUserRentsQuery : IUserAware
    {
        public UserEntity CurrentUser { get; set; }

        public PaginatedData<RentOutput> Execute(Pagination pagination)
        {
            return CurrentUser.Rents
                .Select(r => new RentOutput(r))
                .Paginate(pagination);
        }
    }
}
