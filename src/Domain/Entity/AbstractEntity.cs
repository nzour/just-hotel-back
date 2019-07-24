using System;

namespace app.Domain.Entity
{
    public abstract class AbstractEntity
    {
        public virtual Guid Id { get; private set; }

        protected void Identify()
        {
            Id = Guid.NewGuid();
        }
    }
}