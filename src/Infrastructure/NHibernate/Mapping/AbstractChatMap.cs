using App.Domain.ChatEntity;
using FluentNHibernate.Mapping;

namespace App.Infrastructure.NHibernate.Mapping
{
    public class AbstractChatMap : ClassMap<AbstractChat>
    {
        public AbstractChatMap()
        {
            Table("AbstractChats");

            Id(x => x.Id).Column("Id");

            Map(x => x.CreatedAt)
                .Column("CreatedAt")
                .Not.Nullable();

            Map(x => x.UpdatedAt)
                .Column("UpdatedAt")
                .Not.Nullable();
            
            HasMany(x => x.Messages)
                .KeyColumn("ChatId")
                .Cascade.Persist()
                .Cascade.DeleteOrphan();
            
            HasManyToMany(x => x.Members)
                .Table("UserChats")
                .LazyLoad();

            DiscriminateSubClassesOnColumn<ChatType>("ChatType")
                .ReadOnly()
                .Not.Nullable();
        }
    }
}