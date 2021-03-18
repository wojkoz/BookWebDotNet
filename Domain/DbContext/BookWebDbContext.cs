using BookWebDotNet.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookWebDotNet.Domain.DbContext
{
    public class BookWebDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BookWebDbContext(DbContextOptions<BookWebDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}
