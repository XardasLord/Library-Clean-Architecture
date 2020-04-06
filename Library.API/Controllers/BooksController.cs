using System.Threading.Tasks;
using Library.Application.UseCases.Books.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    public class BooksController : ApiBaseController
    {
        [HttpGet("{id}")]
        public IActionResult GetBook(long id)
        {
            return Ok(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookCommand command)
        {
            var bookId = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetBook), new { id = bookId }, new { bookId });
        }
    }
}
