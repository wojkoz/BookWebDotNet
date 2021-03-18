using BookWebDotNet.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookWebDotNet.Domain.DbContext
{
    public class BookWebDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public BookWebDbContext(DbContextOptions<BookWebDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users;
        public DbSet<Review> Reviews;
        public DbSet<Comment> Comments;
        public DbSet<Book> Books;

    }
}
