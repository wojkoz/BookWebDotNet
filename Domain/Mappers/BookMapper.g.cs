using BookWebDotNet.Domain.Entity;

namespace BookWebDotNet.Domain.Entity
{
    public static partial class BookMapper
    {
        public static BookDto AdaptToDto(this Book p1)
        {
            return p1 == null ? null : new BookDto()
            {
                BookId = p1.BookId,
                Title = p1.Title,
                Author = p1.Author,
                Year = p1.Year,
                Publisher = p1.Publisher,
                Cover = p1.Cover
            };
        }
        public static BookDto AdaptTo(this Book p2, BookDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            BookDto result = p3 ?? new BookDto();
            
            result.BookId = p2.BookId;
            result.Title = p2.Title;
            result.Author = p2.Author;
            result.Year = p2.Year;
            result.Publisher = p2.Publisher;
            result.Cover = p2.Cover;
            return result;
            
        }
    }
}