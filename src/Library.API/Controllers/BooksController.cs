using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.UseCases.Books.Commands;
using Library.Application.UseCases.Books.Dtos;
using Library.Application.UseCases.Books.Queries.GetAvailableBooks;
using Library.Application.UseCases.Books.Queries.GetBook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Authorize]
    public class BooksController : ApiBaseController
    {
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(long id) 
            => Ok(await Mediator.Send(new GetBookQuery(id)));

        [AllowAnonymous]
        [HttpGet("available")]
        public async Task<ActionResult<IReadOnlyCollection<BookDto>>> GetAllAvailableBooks()
            => Ok(await Mediator.Send(new GetAvailableBooksQuery()));

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookCommand command)
        {
            var bookId = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetBook), new { id = bookId }, new { bookId });
        }
    }
}
