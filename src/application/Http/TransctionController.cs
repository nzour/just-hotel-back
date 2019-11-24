using System.Threading.Tasks;
using Application.CQS.Transaction;
using Common.Util;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [ApiController]
    [Route("transactions")]
    public class TransactionController : Controller
    {
        public UserExtractor UserExtractor { get; }

        public TransactionController(UserExtractor userExtractor)
        {
            UserExtractor = userExtractor;
        }

        [HttpGet]
        public PaginatedData<TransactionOutput> GetTransactions(
            [FromServices] GetAllTransactionsQuery query,
            [FromQuery] TransactionFilter filter,
            [FromQuery] Pagination pagination
        )
        {
            return query.Execute(filter, pagination);
        }

        public async Task<PaginatedData<TransactionOutput>> GetCurrentUserTransactions(
            [FromServices] GetAllTransactionsQuery query,
            [FromQuery] TransactionFilter filter,
            [FromQuery] Pagination pagination
        )
        {
            var currentUser = await UserExtractor.ProvideUser();
            filter.UserId = currentUser.Id;

            return query.Execute(filter, pagination);
        }
    }
}
