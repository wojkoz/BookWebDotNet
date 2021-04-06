using System;
using System.ComponentModel.DataAnnotations;
using Mapster;

namespace BookWebDotNet.Domain.Entity
{
    [AdaptTo("[name]Dto"), GenerateMapper]
    public record Review
    {
        [Key]
        public Guid ReviewId { get; init; }
        public Guid BookId { get; init; }
        public Guid UserId { get; init; }
        public string Content { get; init; }
    }
}
