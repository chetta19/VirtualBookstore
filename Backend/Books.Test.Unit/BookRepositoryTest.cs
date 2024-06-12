using Xunit;
using Microsoft.EntityFrameworkCore;
using Books.Repositories;
using Books.Data;
using Books.Models;

namespace Books.Test.Unit
{
    public class BookRepositoryTest
    {
        private BookContext _context;
        private BookRepository _repository;

        public BookRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<BookContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new BookContext(options);
            _repository = new BookRepository(_context);
        }

        [Fact]
        public async Task GetAllBooks_ReturnsAllBooks()
        {
            // Arrange
            var book1 = new Book { Id = "ID1", ISBN = "123", Title = "Book 1", Author = "Author 1" };
            var book2 = new Book { Id = "ID2", ISBN = "456", Title = "Book 2", Author = "Author 2" };
            await _repository.InsertBook(book1);
            await _repository.InsertBook(book2);

            // Act
            var result = await _repository.GetAllBooks();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task InsertBook_AddsBookToDatabase()
        {
            // Arrange
            var book = new Book { Id = "ID1", ISBN = "123", Title = "Book 1", Author = "Author 1" };

            // Act
            await _repository.InsertBook(book);

            // Assert
            Assert.Contains(book, _context.Books);
        }
        [Fact]
        public async Task GetBookByISBN_BookIsFound_ReturnsCorrectBook()
        {
            // Arrange
            var book1 = new Book { Id = "ID1", ISBN = "123", Title = "Book 1", Author = "Author 1" };
            var book2 = new Book { Id = "ID2", ISBN = "456", Title = "Book 2", Author = "Author 2" };
            await _repository.InsertBook(book1);
            await _repository.InsertBook(book2);

            // Act
            var result = await _repository.GetBookByISBN("123");

            // Assert
            Assert.Equal(book1, result);
        }
        [Fact]
        public async Task GetBookByISBN_ReturnsNull_WhenBookNotFound()
        {
            // Arrange
            var book = new Book { Id = "ID1", ISBN = "123", Title = "Book 1", Author = "Author 1" };
            await _repository.InsertBook(book);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetBookByISBN("456"); // ISBN that does not exist

            // Assert
            Assert.Null(result);
        }
    }
}