namespace Library.Application.UseCases.Storages.ViewModels
{
    public class BookViewModel
    {
        public long Id { get; set; }
        public long StorageId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Isbn { get; set; }
        public bool InStock { get; set; }
    }
}