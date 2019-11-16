using System;

namespace Domain
{
    public class EntityNotFoundException<TEntity> : Exception where TEntity : class
    {
        public EntityNotFoundException(object id) : base($"${typeof(TEntity).Name} with identifier {id} was not found.")
        {
        }
    }
}