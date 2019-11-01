using System;

namespace Domain
{
    public abstract class AbstractEntity
    {
        public virtual Guid Id { get; protected set; }

        protected void Identify()
        {
            Id = Guid.NewGuid();
        }
    }
}