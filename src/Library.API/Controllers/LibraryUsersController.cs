using System.Threading.Tasks;
using Library.Application.UseCases.LibraryUsers.Commands.RegisterLibraryUser;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    public class LibraryUsersController : ApiBaseController
    {
        [HttpPost]
        public async Task<IActionResult> RegisterLibraryUser(RegisterLibraryUserCommand command)
        {
            return Ok(await Mediator.Send(command));
            // TODO: CreatedAtAction(nameof(GetLibraryUser), new { id = userId }, new { userId });
        }
    }
}
