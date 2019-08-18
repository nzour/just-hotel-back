using app.Common.Services.Jwt;
using app.Domain.Entity.User;
using cli.Abstraction;

namespace cli.Commands
{
    public class TestCommand : AbstractCommand
    {
        public override void Execute(ICommandArgument argument)
        {
            var a = 1;
        }

        public override string GetName()
        {
            return "app:test";
        }
    }
}