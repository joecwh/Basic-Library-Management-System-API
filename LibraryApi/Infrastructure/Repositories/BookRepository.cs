using LibraryApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryApi.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _db;

        public BookRepository(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _db.Books
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
            return await _db.Books
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Book book)
        {
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _db.Books.Update(book);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesByIdsAsync(List<int> categoryIds)
        {
            return await _db.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .ToListAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _db.Database.BeginTransactionAsync();
        }
    }
}
