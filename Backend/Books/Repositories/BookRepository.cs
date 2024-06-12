using Books.Data;
using Books.Interfaces;
using Books.Models;

namespace Books.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book? GetBookByISBN(string isbn)
        {
            return _context.Books.FirstOrDefault(book => book.ISBN == isbn);
        }

        public void InsertBook(Book book)
        {
            _context.Books.Add(book);
            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}