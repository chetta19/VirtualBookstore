#nullable enable
namespace Books.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
    }
}