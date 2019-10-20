using App.Domain.ChatEntity;
using FluentNHibernate.Mapping;

namespace App.Infrastructure.NHibernate.Mapping
{
    public class SingleChatMap : SubclassMap<SingleChat>
    {
        public SingleChatMap()
        {
            Table("SingleChats");

            DiscriminatorValue(ChatType.Single);
        }
    }
}