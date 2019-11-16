using System.ComponentModel.DataAnnotations;
using Domain.Service;

namespace Application.CQS.Service.Input
{
    public class ServiceInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public uint Cost { get; set; }

        public ServiceInput(string name, uint cost)
        {
            Name = name;
            Cost = cost;
        }

        public ServiceEntity ToEntity()
        {
            return new ServiceEntity(Name, Cost);
        }
    }
}
