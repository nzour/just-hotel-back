using Application.CQS.Transaction;
using Common.Util;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [ApiController]
    [Route("transactions")]
    public class TransactionController : Controller
    {
        [HttpGet]
        public PaginatedData<TransactionOutput> GetTransactions(
            [FromServices] GetAllTransactionsQuery query,
            [FromQuery] TransactionFilter filter,
            [FromQuery] Pagination pagination
        )
        {
            return query.Execute(filter, pagination);
        }
    }
}
