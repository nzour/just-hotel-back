using System;
using App.Domain.ChatEntity;
using App.Domain.UserEntity;

#nullable disable

namespace App.Domain.MessageEntity
{
    public class Message : AbstractEntity
    {
        public AbstractChat Chat { get; private set; }
        public User Sender { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Message(AbstractChat chat, User sender, string content)
        {
            Identify();

            Chat = chat;
            Sender = sender;
            Content = content;
            
            CreatedAt = new DateTime();
        }
    }
}