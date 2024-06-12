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
            await _repository.InsertBook(BooksTestData.Book1);
            await _repository.InsertBook(BooksTestData.Book2);

            // Act
            var result = await _repository.GetAllBooks();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task InsertBook_AddsBookToDatabase()
        {
            // Act
            await _repository.InsertBook(BooksTestData.Book1);

            // Assert
            Assert.Contains(BooksTestData.Book1, _context.Books);
        }
        [Fact]
        public async Task GetBookByISBN_BookIsFound_ReturnsCorrectBook()
        {
            // Arrange
            await _repository.InsertBook(BooksTestData.Book1);
            await _repository.InsertBook(BooksTestData.Book2);

            // Act
            var result = await _repository.GetBookByISBN(BooksTestData.Book1.ISBN);

            // Assert
            Assert.Equal(BooksTestData.Book1, result);
        }
        [Fact]
        public async Task GetBookByISBN_ReturnsNull_WhenBookNotFound()
        {
            // Arrange
            await _repository.InsertBook(BooksTestData.Book1);

            // Act
            var result = await _repository.GetBookByISBN("456"); // ISBN that does not exist

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBookById_BookIsFound_ReturnsCorrectBook()
        {
            // Arrange
            await _repository.InsertBook(BooksTestData.Book1);
            await _repository.InsertBook(BooksTestData.Book2);

            // Act
            var result = await _repository.GetBookById(BooksTestData.Book1.Id);

            // Assert
            Assert.Equal(BooksTestData.Book1, result);
        }

        [Fact]
        public async Task GetBookById_ReturnsNull_WhenBookNotFound()
        {
            // Arrange
            await _repository.InsertBook(BooksTestData.Book1);

            // Act
            var result = await _repository.GetBookById("ID2"); // ID that does not exist

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task UpdateBook_UpdatesBookDetails()
        {
            // Arrange
            await _repository.InsertBook(BooksTestData.Book1);

            // Act
            var bookToUpdate = BooksTestData.Book1;
            bookToUpdate.Title = "Updated Title";
            await _repository.UpdateBook(bookToUpdate);
            var updatedBook = await _repository.GetBookById(bookToUpdate.Id);

            // Assert
            Assert.NotNull(updatedBook);
            Assert.Equal("Updated Title", updatedBook.Title);
        }
        
        [Fact]
        public async Task UpdateBook_ThrowsArgumentException_WhenBookNotFound()
        {
            // Arrange
            var bookToUpdate = BooksTestData.Book1;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _repository.UpdateBook(bookToUpdate));
        }
    }
}