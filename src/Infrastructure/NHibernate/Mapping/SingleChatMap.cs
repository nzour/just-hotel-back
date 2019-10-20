using App.Domain.ChatEntity;
using FluentNHibernate.Mapping;

namespace App.Infrastructure.NHibernate.Mapping
{
    public class SingleChatMap : ClassMap<SingleChat>
    {
        public SingleChatMap()
        {
            Table("SingleChat");
            Id(x => x.Id);
        }
    }
}