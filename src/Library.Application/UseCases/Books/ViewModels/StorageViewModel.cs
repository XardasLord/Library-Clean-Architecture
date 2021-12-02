using System.Collections.Generic;

namespace Library.Application.UseCases.Books.ViewModels
{
    public class StorageViewModel
    {
        public long Id { get; set; }
        public IEnumerable<BookViewModel> Books { get; set; }
    }
}