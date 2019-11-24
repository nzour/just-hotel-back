using System;
using System.Collections.Generic;
using System.Linq;
using Application.CQS.Room.Output;
using Application.CQS.Service.Output;
using Application.CQS.User.Output;
using Domain.Transaction;

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

        public TransactionOutput(TransactionEntity transaction)
        {
            Id = transaction.Id;
            Money = transaction.Money;
            User = new UserOutput(transaction.User);
            Room = new RoomOutput(transaction.Room);
            CreatedAt = transaction.CreatedAt;
            Services = transaction.Services.Select(s => new ServiceOutput(s));
        }
    }
}
