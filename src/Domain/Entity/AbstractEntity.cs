using System;

namespace app.Domain.Entity
{
    public class AbstractEntity
    {
        protected Guid Id { get; set; }

        protected AbstractEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid GetId()
        {
            return Id;
        }

        public string GetIdAsString()
        {
            return Id.ToString();
        }
    }
}