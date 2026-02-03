using LibraryApi.Application.Dtos;
using LibraryApi.Domain.Entities;

namespace LibraryApi.Application.Interfaces
{
    public interface IBookService
    {
        Task<List<BookResponseModel>> GetAllBooksAsync();
        Task<BookResponseModel> GetBookByIdAsync(Guid id);
        Task<ResultResponseModel> AddBookAsync(AddBookRequestModel model);
        Task<ResultResponseModel> BorrowBookAsync(Guid bookId);
        Task<ResultResponseModel> ReturnBookAsync(Guid bookId);
        Task<List<Category>> GetCategories();
    }
}
