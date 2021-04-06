using System;

namespace BookWebDotNet.Domain.Dtos
{
    public record CreateReviewDto
    {
        public Guid BookId { get; init; }
        public Guid UserId { get; init; }
        public string Content { get; init; }
    }
}
