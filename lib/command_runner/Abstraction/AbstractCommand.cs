using command_runner.CommandHandler;
using command_runner.Handler;

namespace command_runner.Abstraction
{
    public abstract class AbstractCommand
    {
        public abstract void Execute(ArgumentProvider provider);

        public abstract string GetName();
    }
}