namespace Library.Application.UseCases.Exceptions
{
    public class LibraryUserAlreadyExistsException : ApplicationException
    {
        protected override string Code => "library_user_already_exists";
        public string Email { get; }

        public LibraryUserAlreadyExistsException(string email) : base($"Library user already exists with email {email}.") 
            => Email = email;
    }
}
