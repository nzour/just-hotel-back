using Domain.Transaction;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping
{
    public class TransactionMap : ClassMap<TransactionEntity>
    {
        public TransactionMap()
        {
            Id(x => x.Id);
            Table("Transactions");

            Map(x => x.Money)
                .Not.Nullable();

            Map(x => x.CreatedAt)
                .Not.Nullable();

            References(x => x.User, "UserId")
                .Cascade.Delete()
                .Not.Nullable();

            References(x => x.Room, "RoomId")
                .Cascade.Delete()
                .Not.Nullable();

            HasManyToMany(x => x.Services)
                .ParentKeyColumn("TransactionId")
                .ChildKeyColumn("ServiceId")
                .Table("TransactionService");
        }
    }
}
