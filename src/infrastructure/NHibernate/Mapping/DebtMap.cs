using Domain.Debt;
using Domain.DebtEntity;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping
{
    public class DebtMap : ClassMap<DebtEntity>
    {
        public DebtMap()
        {
            Not.LazyLoad();

            Id(x => x.Id);
            Table("Debts");

            Map(x => x.HandedAt).Nullable();
            Map(x => x.Money).Not.Nullable();

            References(x => x.Debtor)
                .Column("DebtorId")
                .Not.Nullable();

            References(x => x.RoomEntity)
                .Column("RoomId")
                .Not.Nullable();
        }
    }
}