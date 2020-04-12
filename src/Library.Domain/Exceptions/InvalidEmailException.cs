namespace Library.Domain.Exceptions
{
    public class InvalidEmailException : DomainException
    {
        protected override string Code => "invalid_email_format";
        public string Email { get; }

        public InvalidEmailException(string email, string message) : base(message) 
            => Email = email;
    }
}