using System;
using Mapster;

namespace BookWebDotNet.Domain.Entity
{
    [AdaptTo("[name]Dto"), GenerateMapper]
    public record Comment
    {
        public Guid CommentId { get; init; }
        public Guid BookId { get; init; }
        public Guid UserId { get; init; }
        public string Content { get; init; }
    }
}
