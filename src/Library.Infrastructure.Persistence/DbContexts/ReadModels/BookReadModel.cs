namespace Library.Infrastructure.Persistence.DbContexts.ReadModels
{
    public class BookReadModel
    {
        public long Id { get; set; }
        public string Title { get; set;  }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Isbn { get; set; }
        public bool InStock { get; set; }
    }
}