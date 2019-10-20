using System.Collections.Generic;
using System.Linq;
using App.Domain.MessageEntity;
using App.Domain.UserEntity;

namespace App.Domain.ChatEntity
{
    public class GroupChat : AbstractChat
    {
        public User Creator { get; }
        public IEnumerable<User> Members { get; private set; }

        public GroupChat(User creator, IEnumerable<User> members) : base(ChatType.Group)
        {
            Creator = creator;
            Members = members;
        }

        public override bool CanAddMessage(Message message)
        {
            return Members.Contains(message.Sender);
        }
    }
}