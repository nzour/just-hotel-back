using System;

namespace app.Modules.ORM.Entities
{
    abstract public class AbstractEntity
    {
        public virtual Guid Id { get; protected set; }

        public virtual bool IsEquals(AbstractEntity abstractEntity)
        {
            return Id.Equals(abstractEntity.Id);
        }

        public virtual void Initialize()
        {
            Id = Guid.NewGuid();
        }
    }
}