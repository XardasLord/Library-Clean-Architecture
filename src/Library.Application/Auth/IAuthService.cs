namespace Library.Application.Auth
{
    public interface IAuthService
    {
        string GenerateSecurityToken(string email, string name);
    }
}
