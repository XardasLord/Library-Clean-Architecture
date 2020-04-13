namespace Library.Application.Auth
{
    public interface IAuthService
    {
        string GenerateSecurityToken(long userId, string email, string name);
    }
}
