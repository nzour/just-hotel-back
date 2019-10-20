using System;
using System.Collections.Generic;
using System.Linq;
using App.Domain.MessageEntity;

#nullable disable

namespace App.Domain.ChatEntity
{
    public abstract class AbstractChat : AbstractEntity
    {
        public ChatType ChatType { get; private set; }
        public IEnumerable<Message> Messages { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public abstract bool CanAddMessage(Message message);

        protected AbstractChat(ChatType type)
        {
            Identify();

            ChatType = type;
            Messages = new List<Message>();
            CreatedAt = new DateTime();
        }

        public void AddMessage(Message message)
        {
            if (!CanAddMessage(message))
            {
                throw CantAddMessageException.UserHasNoAccessForChat();
            }

            Messages.Append(message);
        }
    }

    public enum ChatType
    {
        Single = 0,
        Group = 1
    }
}