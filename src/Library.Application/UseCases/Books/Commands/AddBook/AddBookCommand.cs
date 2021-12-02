using MediatR;

namespace Library.Application.UseCases.Books.Commands.AddBook
{
    public class AddBookCommand : IRequest<long>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Isbn { get; set; }
    }
}
