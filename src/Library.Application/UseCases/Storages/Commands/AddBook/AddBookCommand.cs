using MediatR;

namespace Library.Application.UseCases.Storages.Commands.AddBook
{
    public class AddBookCommand : IRequest<long>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Isbn { get; set; }
    }
}
