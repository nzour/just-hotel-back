using System;

namespace Domain.Entities
{
    public class ServiceEntity
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public int Cost { get; set; }

        public ServiceEntity(string name, uint cost)
        {
            Id = Guid.NewGuid();
            Name = name;
            Cost = (int) cost;
        }
    }
}
