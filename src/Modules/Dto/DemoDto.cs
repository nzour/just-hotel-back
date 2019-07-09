using System;

namespace app.Modules.Dto
{
    public class DemoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DemoDto(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}