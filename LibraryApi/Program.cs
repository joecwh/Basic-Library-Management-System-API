using LibraryApi.Application.Interfaces;
using LibraryApi.Application.Mappers;
using LibraryApi.Application.Services;
using LibraryApi.Domain.Entities;
using LibraryApi.Infrastructure;
using LibraryApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddAutoMapper(typeof(BookMappingProfile));

// -------- OPTION 1: IN-MEMORY (FOR LOCAL TESTING) --------
// Active configuration for now
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseInMemoryDatabase("LibraryInMemoryDb"));

// -------- OPTION 2: SQL SERVER (PRODUCTION / REAL DB) --------
// Uncomment this when you are ready to use SQL Server
/*
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LibraryDb")));
*/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed some sample data for InMemory testing
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

    if (!db.Books.Any())
    {
        // -------- Seed Categories --------
        var categories = new List<Category>
        {
            new Category { Id = 1, CategoryName = "Programming" },
            new Category { Id = 2, CategoryName = "Software Architecture" },
            new Category { Id = 3, CategoryName = "Technology" }
        };

        db.Categories.AddRange(categories);

        // -------- Seed Books --------
        var books = new List<Book>
        {
            new Book("Clean Code", "Robert C. Martin", "Prentice Hall", "9780132350884"),
            new Book("Domain-Driven Design", "Eric Evans", "Addison-Wesley", "9780321125217"),
            new Book("ASP.NET Core in Action", "Andrew Lock", "Manning", "9781617294613")
        };

        db.Books.AddRange(books);
        db.SaveChanges();   // save first to generate Book IDs

        // -------- Seed BookCategory (many-to-many link) --------
        var bookCategories = new List<BookCategory>
        {
            new BookCategory
            {
                BookId = books[0].Id,   // Clean Code
                CategoryId = 1          // Programming
            },
            new BookCategory
            {
                BookId = books[1].Id,   // DDD
                CategoryId = 2          // Software Architecture
            },
            new BookCategory
            {
                BookId = books[2].Id,   // ASP.NET Core in Action
                CategoryId = 1          // Programming
            },
            new BookCategory
            {
                BookId = books[2].Id,   // ASP.NET Core in Action
                CategoryId = 3          // Technology
            }
        };

        db.BookCategories.AddRange(bookCategories);
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
