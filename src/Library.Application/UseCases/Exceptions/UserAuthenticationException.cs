namespace Library.Application.UseCases.Exceptions
{
    public class UserAuthenticationException : ApplicationException
    {
        protected override string Code => "user_authentication_error";

        public UserAuthenticationException(string message) : base(message)
        {
        }
    }
}
