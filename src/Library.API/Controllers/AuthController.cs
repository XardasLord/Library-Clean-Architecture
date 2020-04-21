using System.Threading.Tasks;
using Library.Application.UseCases.Auth.Commands.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    public class AuthController : ApiBaseController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginCommand command)
            => Ok(await Mediator.Send(command));
    }
}
