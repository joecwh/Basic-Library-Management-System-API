namespace LibraryApi.Application.Dtos
{
    public class ResultResponseModel
    {
        public bool IsSuccess { get; set; } = false;
        public string? Message { get; set; }
    }
}
