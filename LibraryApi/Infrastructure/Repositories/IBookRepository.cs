using LibraryApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryApi.Infrastructure.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(Guid id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task<List<Category>> GetCategoriesAsync();
        Task<List<Category>> GetCategoriesByIdsAsync(List<int> categoryIds);

        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
