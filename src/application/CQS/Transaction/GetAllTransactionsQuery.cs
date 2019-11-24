using System.Linq;
using Common.Extensions;
using Common.Util;
using Domain;
using Domain.Entities;

namespace Application.CQS.Transaction
{
    public class GetAllTransactionsQuery
    {
        private IEntityRepository<Domain.Entities.TransactionEntity> TransactionRepository { get; }

        public GetAllTransactionsQuery(IEntityRepository<Domain.Entities.TransactionEntity> transactionRepository)
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
