#nullable enable
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Books.Models
{
    public class Book
    {
        [BsonId]
        public required string Id { get; set; }
        public required string ISBN { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; } = 0;
        public string? CoverImage { get; set; }
        public string? Description { get; set; }
    }
}