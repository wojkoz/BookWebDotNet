using System;
using System.ComponentModel.DataAnnotations;

namespace BookWebDotNet.Domain.Entity
{
    public record Book
    {
        [Key]
        public Guid BookId { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public int Year { get; init; }
        public string Publisher { get; init; }
        public string Cover { get; init; }
    }
}
