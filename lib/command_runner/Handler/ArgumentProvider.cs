using System;
using command_runner.CommandHandler.Exception;

namespace command_runner.Handler
{
    public class ArgumentProvider
    {
        private string[] DefinedArguments { get; }
        private int Position { get; set; }
        
        public ArgumentProvider(string[] arguments)
        {
            DefinedArguments = arguments;
            Position = 0;
        }

        public bool HasNext()
        {
            return DefinedArguments.Length > Position;
        }

        public int NextAsInt()
        {
            return Next(() => Convert.ToInt32(DefinedArguments[Position]));
        }

        public string NextAsString()
        {
            return Next(() => Convert.ToString(DefinedArguments[Position]));
        }

        public double NextAsDouble()
        {
            return Next(() => Convert.ToDouble(DefinedArguments[Position]));
        }

        public bool NextAsBool()
        {
            return Next(() => Convert.ToBoolean(DefinedArguments[Position]));
        }

        private void AssertHasNext()
        {
            if (!HasNext())
            {
                throw CommandArgumentException.NotFound(Position);
            }   
        }

        private T Next<T>(Func<T> action)
        {
            AssertHasNext();
            
            try
            {
                return action.Invoke();
            }
            finally
            {
                Position++;
            }
        }
    }
}