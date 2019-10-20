using App.Domain.ChatEntity;
using FluentNHibernate.Mapping;

namespace App.Infrastructure.NHibernate.Mapping
{
    public class GroupChatMap : SubclassMap<GroupChat>
    {
        public GroupChatMap()
        {
            Table("GroupChats");

            References(x => x.Creator)
                .Column("CreatorId")
                .Not.Nullable();

            DiscriminatorValue(ChatType.Group);
        }
    }
}