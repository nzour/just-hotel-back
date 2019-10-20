using App.Domain.MessageEntity;
using FluentNHibernate.Mapping;

namespace App.Infrastructure.NHibernate.Mapping
{
    public class MessageMap : ClassMap<Message>
    {
        public MessageMap()
        {
            Table("Messages");
            Id(x => x.Id);

            Map(x => x.Content)
                .Column("Content")
                .Not.Nullable();

            Map(x => x.CreatedAt)
                .Column("CreatedAt")
                .Not.Nullable();

            Map(x => x.UpdatedAt)
                .Column("UpdatedAt")
                .Nullable();

            References(x => x.Sender).Column("UserId");
            References(x => x.Chat).Column("ChatId");
        }
    }
}