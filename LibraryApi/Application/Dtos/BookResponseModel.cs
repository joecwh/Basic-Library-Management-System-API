namespace LibraryApi.Application.Dtos
{
    public class BookResponseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public List<string> Categories { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }
    }
}
