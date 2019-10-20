using System.Collections.Generic;
using App.Domain.UserEntity;

#nullable disable

namespace App.Domain.ChatEntity
{
    public class GroupChat : AbstractChat
    {
        public User Creator { get; }
        public GroupChat(User creator, IEnumerable<User> members) : base(ChatType.Group)
        {
            Creator = creator;
            Members = members;
        }
    }
}