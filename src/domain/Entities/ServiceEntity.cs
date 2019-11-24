namespace Domain.Entities
{
    public class ServiceEntity : AbstractEntity
    {
        public string Name { get; set; }
        public uint Cost { get; set; }

        public ServiceEntity(string name, uint cost)
        {
            Identify();
            Name = name;
            Cost = cost;
        }
    }
}
