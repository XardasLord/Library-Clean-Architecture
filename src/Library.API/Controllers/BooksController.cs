using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.UseCases.Storages.Commands.AddBook;
using Library.Application.UseCases.Storages.Commands.BorrowBook;
using Library.Application.UseCases.Storages.Commands.ReturnBook;
using Library.Application.UseCases.Storages.Dtos;
using Library.Application.UseCases.Storages.Queries.GetAvailableBooks;
using Library.Application.UseCases.Storages.Queries.GetBook;
using Library.Application.UseCases.Storages.Queries.GetBookByIsbn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Authorize]
    public class BooksController : ApiBaseController
    {
        [AllowAnonymous]
        [HttpGet("{id:long}")]
        public async Task<ActionResult<BookDto>> GetBook(long id) 
            => Ok(await Mediator.Send(new GetBookQuery(id)));

        [AllowAnonymous]
        [HttpGet("available")]
        public async Task<ActionResult<IReadOnlyCollection<BookDto>>> GetAllAvailableBooks()
            => Ok(await Mediator.Send(new GetAvailableBooksQuery()));

        [AllowAnonymous]
        [HttpGet("{isbn}")]
        public async Task<ActionResult<BookDto>> GetBookByIsbn(string isbn)
            => Ok(await Mediator.Send(new GetBookByIsbnQuery(isbn)));

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookCommand command)
        {
            var bookId = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetBook), new { id = bookId }, new { bookId });
        }

        [HttpPost("{bookId}/borrow")]
        public async Task<IActionResult> BorrowBook(long bookId, BorrowBookCommand command)
        {
            command.BookId = bookId;

            await Mediator.Send(command);

            return Accepted();
        }

        [HttpPost("{bookId}/return")]
        public async Task<IActionResult> ReturnBook(long bookId)
        {
            await Mediator.Send(new ReturnBookCommand(bookId));

            return Accepted();
        }
    }
}
