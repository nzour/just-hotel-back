using System.Collections.Generic;

namespace App.Domain.ChatEntity
{
    public interface IChatRepository : IEntityRepository<AbstractChat>
    {
        IList<AbstractChat> FindAll();
    }
}