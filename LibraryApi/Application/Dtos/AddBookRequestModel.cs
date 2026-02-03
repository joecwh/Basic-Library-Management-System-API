namespace LibraryApi.Application.Dtos
{
    public class AddBookRequestModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public List<int> CategoryIds { get; set; } = new();
    }
}
