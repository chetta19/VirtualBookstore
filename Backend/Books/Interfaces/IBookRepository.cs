using Books.Models;

namespace Books.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();

        Task<Book?> GetBookByISBN(string isbn);

        Task<Book?> GetBookById(string id);


        Task InsertBook(Book book);
    }
}