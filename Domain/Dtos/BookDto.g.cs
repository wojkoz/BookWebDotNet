using System;

namespace BookWebDotNet.Domain.Entity
{
    public partial class BookDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Publisher { get; set; }
        public string Cover { get; set; }
    }
}