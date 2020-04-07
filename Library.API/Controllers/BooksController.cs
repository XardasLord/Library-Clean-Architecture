using System.Threading.Tasks;
using Library.Application.UseCases.Books.Commands;
using Library.Application.UseCases.Books.Queries.GetBook;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    public class BooksController : ApiBaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(long id) 
            => Ok(await Mediator.Send(new GetBookQuery(id)));

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookCommand command)
        {
            var bookId = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetBook), new { id = bookId }, new { bookId });
        }
    }
}
