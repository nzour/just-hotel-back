using System;

namespace app.Domain.Entity
{
    public abstract class AbstractEntity
    {
        public  virtual Guid Id { get; set; }

        protected AbstractEntity()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid GetId()
        {
            return Id;
        }

        public virtual string GetIdAsString()
        {
            return Id.ToString();
        }
    }
}