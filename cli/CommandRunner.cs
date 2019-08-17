using System;
using app;
using kernel;

namespace cli
{
    internal static class CommandRunner
    {
        public static readonly Kernel Kernel = new Kernel(typeof(Startup).Assembly);

        public static void Main(string[] args)
        {
            Console.Write("Hello world!");
        }
    }
}