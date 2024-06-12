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
        public void When_GetAllBooks_ReturnsAllBooks()
        {
            // Arrange
            var book1 = new Book { Id = "ID1", ISBN = "123", Title = "Book 1", Author = "Author 1" };
            var book2 = new Book { Id = "ID2", ISBN = "456", Title = "Book 2", Author = "Author 2" };
            _context.Books.Add(book1);
            _context.Books.Add(book2);
            _context.SaveChanges();

            // Act
            var result = _repository.GetAllBooks();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void When_InsertBook_AddsBookToDatabase()
        {
            // Arrange
            var book = new Book { Id = "ID1", ISBN = "123", Title = "Book 1", Author = "Author 1" };

            // Act
            _repository.InsertBook(book);
            _context.SaveChanges();

            // Assert
            Assert.Contains(book, _context.Books);
        }
    }
}