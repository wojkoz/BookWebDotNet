using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BookWebDotNet.Domain.Dtos
{
    public record CreateUserDto
    {
        [NotNull]
        public string Name { get; init; }
        [NotNull]
        public string Surname { get; init; }
        [NotNull]
        public string Email { get; init; }
        public bool IsAdmin { get; init; }
        [NotNull]
        [MinLength(5)]
        [MaxLength(50)]
        public string Password { get; init; }
    }
}
