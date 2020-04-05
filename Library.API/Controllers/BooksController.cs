using System.Threading.Tasks;
using Library.Application.UseCases.Books.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{id}")]
        public IActionResult GetBook(long id)
        {
            return Ok(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookCommand command)
        {
            var bookId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetBook), new { id = bookId }, new { bookId });
        }
    }
}
