using Domain.DebtEntity;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping
{
    public class DebtMap : ClassMap<Debt>
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

            References(x => x.Room)
                .Column("RoomId")
                .Not.Nullable();
        }
    }
}