using System;

namespace BookWebDotNet.Domain.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException()
        {
            
        }

        public EntityAlreadyExistsException(string message) : base(message)
        {
            
        }
    }
}
