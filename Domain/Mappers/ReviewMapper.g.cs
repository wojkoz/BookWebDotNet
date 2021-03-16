using BookWebDotNet.Domain.Entity;

namespace BookWebDotNet.Domain.Entity
{
    public static partial class ReviewMapper
    {
        public static ReviewDto AdaptToDto(this Review p1)
        {
            return p1 == null ? null : new ReviewDto()
            {
                ReviewId = p1.ReviewId,
                BookId = p1.BookId,
                UserId = p1.UserId,
                Content = p1.Content
            };
        }
        public static ReviewDto AdaptTo(this Review p2, ReviewDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            ReviewDto result = p3 ?? new ReviewDto();
            
            result.ReviewId = p2.ReviewId;
            result.BookId = p2.BookId;
            result.UserId = p2.UserId;
            result.Content = p2.Content;
            return result;
            
        }
    }
}