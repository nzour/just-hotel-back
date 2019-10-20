using System.Linq;
using App.Domain.UserEntity;

#nullable disable

namespace App.Domain.ChatEntity
{
    public class SingleChat : AbstractChat
    {
        public User First => Members.First();
        public User Second => Members.Last();

        public SingleChat(User first, User second) : base(ChatType.Single)
        {
            Members
                .Append(first)
                .Append(second);
        }
    }
}