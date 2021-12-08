using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using JetBrains.Annotations;
using Library.Domain.AggregateModels.BookAggregate.Exceptions;

namespace Library.Domain.AggregateModels.BookAggregate.Guards
{
    public static class IsbnCorrectnessExtension
    {
        // https://howtodoinjava.com/regex/java-regex-validate-international-standard-book-number-isbns/
        private static readonly Regex Isbn10FormatPattern = new(@"^(?:ISBN(?:-10)?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$)[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9X]$");
        private static readonly Regex Isbn13FormatPattern = new(@"^(?:ISBN(?:-13)?:? )?(?=[0-9]{13}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)97[89][- ]?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9]$");
        
        public static string IsbnCorrectness(this IGuardClause guardClause, [CanBeNull] string input, string parameterName, string? message = null)
        {
            if (input != null && !Isbn10FormatPattern.IsMatch(input) && !Isbn13FormatPattern.IsMatch(input))
                throw new BookIsbnInvalidFormatException(input);

            return input;
        }
    }
}