using System;
using System.Collections.Generic;
using System.Linq;
using App.Domain.MessageEntity;
using App.Domain.UserEntity;

#nullable disable

namespace App.Domain.ChatEntity
{
    public abstract class AbstractChat : AbstractEntity
    {
        public ChatType ChatType { get; private set; }
        public IEnumerable<User> Members { get; protected set; }
        public IEnumerable<Message> Messages { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        protected AbstractChat(ChatType type)
        {
            Identify();

            ChatType = type;
            Messages = new List<Message>();
            CreatedAt = new DateTime();
            UpdatedAt = new DateTime();
        }

        public Message AddMessage(User sender, string content)
        {
            if (!Members.Contains(sender))
            {
                throw CantAddMessageException.UserHasNoAccessForChat();
            }

            var message = new Message(this, sender, content);
            Messages.Append(message);

            UpdatedAt = new DateTime();

            return message;
        }
    }

    public enum ChatType
    {
        Single = 0,
        Group = 1
    }
}