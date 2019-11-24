using System.Linq;
using Common.Extensions;
using Common.Util;
using Domain;
using Domain.Transaction;

namespace Application.CQS.Transaction
{
    public class GetAllTransactionsQuery
    {
        private IEntityRepository<TransactionEntity> TransactionRepository { get; }

        public GetAllTransactionsQuery(IEntityRepository<TransactionEntity> transactionRepository)
        {
            TransactionRepository = transactionRepository;
        }

        public PaginatedData<TransactionOutput> Execute(TransactionFilter filter, Pagination pagination)
        {
            return TransactionRepository.FindAll()
                .ApplyFilter(filter)
                .Select(t => new TransactionOutput(t))
                .Paginate(pagination);
        }
    }
}
