using Books.Models;

namespace Books.Test.Unit
{
    public static class BooksTestData
    {
        public static Book Book1 = new Book { Id = "ID1", ISBN = "123", Title = "Book 1", Author = "Author 1" };
        public static Book Book2 = new Book { Id = "ID2", ISBN = "456", Title = "Book 2", Author = "Author 2" };
    }
}