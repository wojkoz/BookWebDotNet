using System;

namespace BookWebDotNet.Domain.Entity
{
    public record Comment
    {
        public Guid CommentId { get; init; }
        public Guid BookId { get; init; }
        public Guid UserId { get; init; }
        public string Content { get; init; }
    }
}
