namespace Library.Application.UseCases.Storages.Dtos
{
    public class BookDto
    {
        public string Title { get; set;  }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Isbn { get; set; }
        public bool InStock { get; set; }
    }
}
