using System;

namespace BookWebDotNet.Domain.Entity
{
    public partial class CommentDto
    {
        public Guid CommentId { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
    }
}