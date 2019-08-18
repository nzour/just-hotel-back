using System;
using System.Linq;
using System.Reflection;
using kernel.Abstraction;
using Ninject;

namespace cli.Abstraction
{
    public abstract class AbstractCommand
    {
        public ParameterInfo Argument => GetType().GetMethod("Execute")?.GetParameters().First();

        public abstract void Execute(ICommandArgument argument);

        public abstract string GetName();
    }
}