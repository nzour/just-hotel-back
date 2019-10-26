using System;

namespace Root.Configuration.ServiceRecorder.Exception
{
    public class RepositoryRecorderException : System.Exception
    {
        public RepositoryRecorderException(string message) : base(message)
        {
        }

        public static RepositoryRecorderException NotFoundImplementation(Type @interface)
        {
            return new RepositoryRecorderException($"Repository {@interface.Name} has no implementation class.");
        }
    }
}