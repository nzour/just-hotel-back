using System;
using App.Configuration.ServiceRecorder;

namespace App.Domain
{
    [IgnoreMapping]
    public abstract class AbstractEntity
    {
        public virtual Guid Id { get; private set; }

        protected void Identify()
        {
            Id = Guid.NewGuid();
        }
    }
}