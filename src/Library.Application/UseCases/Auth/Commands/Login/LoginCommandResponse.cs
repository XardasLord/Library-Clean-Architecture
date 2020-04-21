namespace Library.Application.UseCases.Auth.Commands.Login
{
    public class LoginCommandResponse
    {
        public string Token { get; }

        public LoginCommandResponse(string token) => Token = token;
    }
}
