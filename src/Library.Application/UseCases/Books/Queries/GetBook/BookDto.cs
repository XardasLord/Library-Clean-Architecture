namespace Library.Application.UseCases.Books.Queries.GetBook
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
