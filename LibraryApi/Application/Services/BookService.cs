using AutoMapper;
using LibraryApi.Application.Dtos;
using LibraryApi.Application.Interfaces;
using LibraryApi.Domain.Entities;
using LibraryApi.Infrastructure.Repositories;

namespace LibraryApi.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<BookResponseModel>> GetAllBooksAsync()
        {
            var books = await _repo.GetAllAsync();
            return _mapper.Map<List<BookResponseModel>>(books);
        }

        public async Task<BookResponseModel?> GetBookByIdAsync(Guid id)
        {
            var book = await _repo.GetByIdAsync(id);

            if (book == null) return null;

            return _mapper.Map<BookResponseModel>(book);
        }

        public async Task<ResultResponseModel> AddBookAsync(AddBookRequestModel model)
        {
            var response = new ResultResponseModel();
            var existingCategories = await _repo.GetCategoriesByIdsAsync(model.CategoryIds);

            if (existingCategories.Count != model.CategoryIds.Count)
            {
                response.Message = "One or more categories not found";
                return response;
            }

            var book = new Book(model.Title, model.Author, model.Publisher, model.ISBN);

            foreach (var catId in model.CategoryIds)
            {
                book.BookCategories.Add(new BookCategory
                {
                    BookId = book.Id,
                    CategoryId = catId
                });
            }

            await _repo.AddAsync(book);

            response.IsSuccess = true;
            return response;
        }

        public async Task<ResultResponseModel> BorrowBookAsync(Guid bookId)
        {
            var response = new ResultResponseModel();
            var book = await _repo.GetByIdAsync(bookId);

            if (book == null)
            {
                response.Message = "Book not found";
                return response;
            }

            if (!book.IsAvailable)
            {
                response.Message = "Book not available";
                return response;
            }

            // Uncomment transaction if usnig sqlserver
            //var transaction = await _repo.BeginTransactionAsync();

            try
            {
                book.IsAvailable = false;
                await _repo.UpdateAsync(book);
                //await transaction.CommitAsync();

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                //await transaction.RollbackAsync();
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResultResponseModel> ReturnBookAsync(Guid bookId)
        {
            var response = new ResultResponseModel();
            var book = await _repo.GetByIdAsync(bookId);

            if (book == null)
            {
                response.Message = "Book not found";
                return response;
            }

            // Uncomment transaction if usnig sqlserver
            //var transaction = await _repo.BeginTransactionAsync();

            try
            {
                book.IsAvailable = true;
                await _repo.UpdateAsync(book);
                //await transaction.CommitAsync();

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                //await transaction.RollbackAsync();
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<List<Category>> GetCategories()
            => await _repo.GetCategoriesAsync();
    }
}
