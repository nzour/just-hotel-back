using System;

namespace App.Domain
{
    public class EntityNotFoundException<TEntity> : Exception where TEntity : class
    {
        public EntityNotFoundException(Guid id) : base($"${typeof(TEntity).Name} with identifier {id} was not found.")
        {
        }
    }
}