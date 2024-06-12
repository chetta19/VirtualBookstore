using Books.Models;

namespace Books.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();

        Book? GetBookByISBN(string isbn);

        void InsertBook(Book book);
    }
}