using BookWebDotNet.Domain.Entity;

namespace BookWebDotNet.Domain.Entity
{
    public static partial class CommentMapper
    {
        public static CommentDto AdaptToDto(this Comment p1)
        {
            return p1 == null ? null : new CommentDto()
            {
                CommentId = p1.CommentId,
                BookId = p1.BookId,
                UserId = p1.UserId,
                Content = p1.Content
            };
        }
        public static CommentDto AdaptTo(this Comment p2, CommentDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            CommentDto result = p3 ?? new CommentDto();
            
            result.CommentId = p2.CommentId;
            result.BookId = p2.BookId;
            result.UserId = p2.UserId;
            result.Content = p2.Content;
            return result;
            
        }
    }
}