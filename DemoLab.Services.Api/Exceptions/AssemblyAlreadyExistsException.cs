using System;

namespace DemoLab.Services.Exceptions
{
    public class AssemblyAlreadyExistsException : Exception
    {
        public AssemblyAlreadyExistsException()
        {
        }

        public AssemblyAlreadyExistsException(string message)
            : base(message)
        {
        }

        public AssemblyAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
