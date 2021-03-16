using System;
using Mapster;

namespace BookWebDotNet.Domain.Entity
{
    [AdaptTo("[name]Dto"), GenerateMapper]
    public record User
    {
        public Guid UserId { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public bool IsAdmin { get; init; }
        [AdaptIgnore]
        public string Password { get; init; }
    }
}
