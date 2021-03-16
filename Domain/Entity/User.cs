using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWebDotNet.Domain.Entity
{
    public record User
    {
        public Guid UserId { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public bool IsAdmin { get; init; }
        public string Password { get; init; }
    }
}
