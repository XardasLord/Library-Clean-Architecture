namespace Library.Domain.Exceptions
{
    public class BookIsbnInvalidFormatException : DomainException
    {
        public override string Code => "invalid_isbn_format";
        public string Isbn { get; }

        public BookIsbnInvalidFormatException(string isbn) : base($"ISBN value has to be 10 or 13 length. Passed ISBN number is: {isbn}.") 
            => Isbn = isbn;
    }
}
