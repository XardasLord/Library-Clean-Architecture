using System.Collections.Generic;
using System.Text.RegularExpressions;
using Library.Domain.Exceptions;
using Library.Domain.SeedWork;

namespace Library.Domain.AggregateModels.BookAggregate
{
    public class Isbn : ValueObject
    {
        // https://howtodoinjava.com/regex/java-regex-validate-international-standard-book-number-isbns/
        private static readonly Regex Isbn10FormatPattern = new Regex(@"^(?:ISBN(?:-10)?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$)[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9X]$");
        private static readonly Regex Isbn13FormatPattern = new Regex(@"^(?:ISBN(?:-13)?:? )?(?=[0-9]{13}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)97[89][- ]?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9]$");
        
        public string Value { get; }

        private Isbn() { }

        public Isbn(string isbn)
        {
            const string nonDigitsPattern = "[^.0-9]";
            isbn = Regex.Replace(isbn, nonDigitsPattern, string.Empty);

            if (!Isbn10FormatPattern.IsMatch(isbn) && !Isbn13FormatPattern.IsMatch(isbn))
                throw new BookIsbnInvalidFormatException(isbn);

            Value = isbn;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}