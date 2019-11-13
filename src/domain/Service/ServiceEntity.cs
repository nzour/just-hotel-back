namespace Domain.Service
{
    public class ServiceEntity : AbstractEntity
    {
        public string Name { get; }
        public int Cost { get; }

        public ServiceEntity(string name, int cost)
        {
            Identify();
            Name = name;
            Cost = cost;
        }
    }
}