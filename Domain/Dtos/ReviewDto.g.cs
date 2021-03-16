using System;

namespace BookWebDotNet.Domain.Entity
{
    public partial class ReviewDto
    {
        public Guid ReviewId { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}