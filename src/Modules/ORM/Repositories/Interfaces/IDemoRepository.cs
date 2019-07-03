using app.Modules.Dto;

namespace app.Modules.ORM.Repositories.Interfaces
{
    public interface IDemoRepository
    {
        DemoDto Provide();
    }
}