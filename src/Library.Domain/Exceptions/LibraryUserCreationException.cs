namespace Library.Domain.Exceptions
{
    public class LibraryUserCreationException : DomainException
    {
        public override string Code => "cannot_create_library_user";

        public LibraryUserCreationException(string message) : base(message)
        {
        }
    }
}
