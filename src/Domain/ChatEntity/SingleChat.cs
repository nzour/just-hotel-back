using App.Domain.MessageEntity;
using App.Domain.UserEntity;

namespace App.Domain.ChatEntity
{
    public class SingleChat : AbstractChat
    {
        public User First { get; private set; }
        public User Second { get; private set; }

        public SingleChat(User first, User second) : base(ChatType.Single)
        {
            First = first;
            Second = second;
        }

        public override bool CanAddMessage(Message message)
        {
            return First == message.Sender || Second == message.Sender;
        }
    }
}