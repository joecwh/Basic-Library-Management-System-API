using LibraryApi.Application.Dtos;
using LibraryApi.Application.Interfaces;
using LibraryApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<BookResponseModel>> GetAll()
            => await _service.GetAllBooksAsync();

        [HttpGet("categories")]
        public async Task<List<Category>> GetCategories()
            => await _service.GetCategories();

        [HttpGet("{bookId}")]
        public async Task<BookResponseModel> GetBookById(Guid bookId)
            => await _service.GetBookByIdAsync(bookId);

        [HttpPost]
        public async Task<ResultResponseModel> Create(AddBookRequestModel model)
            => await _service.AddBookAsync(model);

        [HttpPost("{bookId}/borrow")]
        public async Task<ResultResponseModel> Borrow(Guid bookId)
            => await _service.BorrowBookAsync(bookId);

        [HttpPost("{bookId}/return")]
        public async Task<ResultResponseModel> Return(Guid bookId)
            => await _service.ReturnBookAsync(bookId);
    }
}
