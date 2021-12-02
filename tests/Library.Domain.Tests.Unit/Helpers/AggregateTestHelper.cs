using Library.Domain.AggregateModels.BookAggregate;
using Library.Domain.AggregateModels.LibraryUserAggregate;
using Library.Tests.Base;

namespace Library.Domain.Tests.Unit.Helpers
{
    public class AggregateTestHelper : TestBase
    {
        protected static LibraryUser GetLibraryUserAggregate() => PrepareLibraryUserAggregate();
        protected static Book GetBookAggregate() => PrepareBookAggregate();

        private static LibraryUser PrepareLibraryUserAggregate() => new LibraryUser();
        private static Book PrepareBookAggregate() => new Book();

    }
}