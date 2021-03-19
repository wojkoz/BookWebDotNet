using BookWebDotNet.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookWebDotNet.Domain.DbContext
{
    public class BookWebDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BookWebDbContext()
        {
            
        }
        public BookWebDbContext(DbContextOptions<BookWebDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Book> Books { get; set; }

    }
}
