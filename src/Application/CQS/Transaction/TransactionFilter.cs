using System;
using System.Linq;
using Common.Util;
using Domain.Entities;

namespace Application.CQS.Transaction
{
    public class TransactionFilter : IInputFilter<TransactionEntity>
    {
        public Guid? UserId { get; set; }
        public Guid? RoomId { get; set; }

        public IQueryable<TransactionEntity> Process(IQueryable<TransactionEntity> query)
        {
            if (null != UserId)
            {
                query = query.Where(t => UserId == t.User.Id);
            }

            if (null != RoomId)
            {
                query = query.Where(t => RoomId == t.Room.Id);
            }

            return query;
        }
    }
}
