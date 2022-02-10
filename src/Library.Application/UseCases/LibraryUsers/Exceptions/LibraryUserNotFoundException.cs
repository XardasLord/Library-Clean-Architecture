using ApplicationException = Library.Application.UseCases.Exceptions.ApplicationException;

namespace Library.Application.UseCases.LibraryUsers.Exceptions
{
    public class LibraryUserNotFoundException : ApplicationException
    {
        public override string Code => "library_user_not_found";
        public long LibraryUserId { get; }

        public LibraryUserNotFoundException(long libraryUserId) : base($"Library User with ID {libraryUserId} has not been found.") 
            => LibraryUserId = libraryUserId;
    }
}
