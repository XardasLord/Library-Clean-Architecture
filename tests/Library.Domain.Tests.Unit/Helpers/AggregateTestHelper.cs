using System;
using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Domain.SharedKernel;
using Library.Tests.Base;

namespace Library.Domain.Tests.Unit.Helpers
{
    public class AggregateTestHelper : TestBase
    {
        protected static LibraryUser GetValidLibraryUserAggregate() => PrepareLibraryUserAggregate();
        protected static Book GetValidBookAggregate() => PrepareBookAggregate();
        protected static DateTimePeriod GetValidDateTimePeriod() => PrepareValidDateTimePeriod();

        private static LibraryUser PrepareLibraryUserAggregate()
        {
            return LibraryUser.Create(
                new UserCredential("Login", "Password"),
                new Name("FirstName", "Last"),
                new Email("Email@email.com"));
        }

        private static Book PrepareBookAggregate()
        {
            return Book.Create("Title", "Author", "Subject", "9783161484100");
        }
        
        private static DateTimePeriod PrepareValidDateTimePeriod() 
            => DateTimePeriod.Create(DateTime.UtcNow, DateTime.UtcNow.AddDays(7));
    }
}