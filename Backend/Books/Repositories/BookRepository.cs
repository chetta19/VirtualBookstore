using Books.Data;
using Books.Interfaces;
using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookById(string id)
        {
            return await _context.Books.FirstOrDefaultAsync(book => book.Id == id);
        }

        public async Task<Book?> GetBookByISBN(string isbn)
        {
            return await _context.Books.FirstOrDefaultAsync(book => book.ISBN == isbn);
        }

        public async Task InsertBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {
            if(!_context.Books.Any(b => b.Id == book.Id))
            {
                throw new ArgumentException("Book not found");
            }
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}