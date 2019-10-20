using System;
using System.Collections.Generic;
using System.Linq;
using command_runner.Handler.Exception;

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

        public long NextAsLong(IEnumerable<long>? shouldBe = null)
        {
            var value = Next(() => Convert.ToInt64(DefinedArguments[Position]));

            AssertContains(shouldBe, value);

            return value;
        }
        
        public int NextAsInt(IEnumerable<int>? shouldBe = null)
        {
            var value = Next(() => Convert.ToInt32(DefinedArguments[Position]));

            AssertContains(shouldBe, value);

            return value;
        }

        public string NextAsString(IEnumerable<string>? shouldBe = null)
        {
            var value = Next(() => Convert.ToString(DefinedArguments[Position]));
            
            AssertContains(shouldBe, value);

            return value;
        }

        public double NextAsDouble(IEnumerable<double>? shouldBe = null)
        {
            var value = Next(() => Convert.ToDouble(DefinedArguments[Position]));
            
            AssertContains(shouldBe, value);

            return value;
        }

        public bool NextAsBool(IEnumerable<bool>? shouldBe = null)
        {
            var value = Next(() => Convert.ToBoolean(DefinedArguments[Position]));
            
            AssertContains(shouldBe, value);

            return value;
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

        private void AssertHasNext()
        {
            if (!HasNext())
            {
                throw CommandArgumentException.NotFound(Position);
            }   
        }

        private void AssertContains<T>(IEnumerable<T>? expected, T actual)
        {
            if (null == expected)
            {
                return;
            }
            
            if (!expected.Contains(actual))
            {
                throw CommandArgumentException.NotEquals(expected, actual);
            }
        }
    }
}