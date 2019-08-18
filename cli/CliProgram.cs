using app;
using command_runner;
using kernel;

namespace cli
{
    public static class CliProgram
    {
        public static void Main(string[] args)
        {
            var kernel = new Kernel(typeof(Startup).Assembly);
            kernel.Boot();
            var commandRunner = new CommandRunner(kernel, typeof(CliProgram).Assembly);
            
            commandRunner.Execute(string.Join(" ", args));
        }
    }
}