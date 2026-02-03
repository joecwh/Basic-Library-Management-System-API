namespace LibraryApi.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();

        public Book() { }
        public Book(string title, string author, string publisher, string isbn)
        {
            Id = Guid.NewGuid();
            Title = title;
            Author = author;
            Publisher = publisher;
            ISBN = isbn;
        }
    }
}
