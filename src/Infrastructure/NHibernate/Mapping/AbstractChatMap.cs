using App.Domain.ChatEntity;
using FluentNHibernate.Mapping;

namespace App.Infrastructure.NHibernate.Mapping
{
    public class AbstractChatMap : ClassMap<AbstractChat>
    {
        public AbstractChatMap()
        {
            Id(x => x.Id).Column("Id");
            Map(x => x.CreatedAt).Column("CreatedAt");

            DiscriminateSubClassesOnColumn<ChatType>("ChatType")
                .AlwaysSelectWithValue()
                .ReadOnly()
                .Not.Nullable();
        }
    }
}