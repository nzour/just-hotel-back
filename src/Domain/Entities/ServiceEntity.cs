namespace Domain.Entities
{
    public class ServiceEntity : AbstractEntity
    {
        public string Name { get; set; }
        public int Cost { get; set; }

        public ServiceEntity(string name, uint cost)
        {
            Identify();
            Name = name;
            Cost = (int) cost;
        }
    }
}
