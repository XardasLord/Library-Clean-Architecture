using System.Collections.Generic;

namespace Library.Application.UseCases.Storages.ViewModels
{
    public class StorageViewModel
    {
        public long Id { get; set; }
        public IEnumerable<BookViewModel> Books { get; set; }
    }
}