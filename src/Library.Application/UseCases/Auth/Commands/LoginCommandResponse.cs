namespace Library.Application.UseCases.Auth.Commands
{
    public class LoginCommandResponse
    {
        public string Token { get; }

        public LoginCommandResponse(string token) => Token = token;
    }
}
