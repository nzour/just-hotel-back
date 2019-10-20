using System.Collections.Generic;
using System.Linq;
using App.Domain.ChatEntity;
using FluentNHibernate.Conventions;

namespace App.Infrastructure.NHibernate.Repository
{
    public class ChatRepository : EntityRepository<AbstractChat>, IChatRepository
    {
        public ChatRepository(Transactional transactional) : base(transactional)
        {
        }

        public IList<AbstractChat> FindAll()
        {
            return Transactional.Func(session =>
                session.Query<AbstractChat>()
                    .Where(chat => chat.Messages.IsNotEmpty())
                    .OrderBy(chat => chat.UpdatedAt)
                    .ToList()
            );
        }
    }
}