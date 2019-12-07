using System;
using System.Collections.Generic;
using System.Linq;
using Application.CQS.Room.Output;
using Application.CQS.Service.Output;
using Application.CQS.User.Output;

namespace Application.CQS.Transaction
{
    public class TransactionOutput
    {
        public Guid Id { get; }
        public uint Money { get; }
        public UserOutput User { get; }
        public RoomOutput Room { get; }
        public DateTime CreatedAt { get; }
        public IEnumerable<ServiceOutput> Services { get; }

        public TransactionOutput(Domain.Entities.TransactionEntity transactionEntity)
        {
            Id = transactionEntity.Id;
            Money = (uint) transactionEntity.Money;
            User = new UserOutput(transactionEntity.User);
            Room = new RoomOutput(transactionEntity.Room);
            CreatedAt = transactionEntity.CreatedAt;
            Services = transactionEntity.Services.Select(s => new ServiceOutput(s));
        }
    }
}
