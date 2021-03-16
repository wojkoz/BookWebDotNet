using System;

namespace BookWebDotNet.Domain.Entity
{
    public record Review
    {
        public Guid ReviewId { get; init; }
        public Guid BookId { get; init; }
        public Guid UserId { get; init; }
        public string Content { get; init; }
    }
}
