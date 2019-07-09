using app.Modules.Dto;
using app.Modules.ORM.Repositories.Interfaces;

namespace app.Modules.ORM.Repositories.Implementations
{
    public class DemoRepository : IDemoRepository
    {
        public DemoDto Provide()
        {
            return new DemoDto("Zobor");
        }
    }
}