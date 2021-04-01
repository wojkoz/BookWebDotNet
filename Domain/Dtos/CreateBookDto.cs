using System.Diagnostics.CodeAnalysis;

namespace BookWebDotNet.Domain.Dtos
{
    public record CreateBookDto
    {
        [NotNull]
        public string Title { get; init; }
        [NotNull]
        public string Author { get; init; }
        [NotNull]
        public int Year { get; init; }
        [NotNull]
        public string Publisher { get; init; }
        public string Cover { get; init; }
    }
}
