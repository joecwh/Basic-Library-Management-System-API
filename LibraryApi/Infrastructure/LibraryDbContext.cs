using LibraryApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options) { }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<BookCategory> BookCategories => Set<BookCategory>();
    }
}
