using System;

namespace BookWebDotNet.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
            
        }

        public EntityNotFoundException(string message) : base(message)
        {
            
        }
    }
}
