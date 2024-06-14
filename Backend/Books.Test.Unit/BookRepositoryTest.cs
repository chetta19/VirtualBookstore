using Xunit;
using Microsoft.EntityFrameworkCore;
using Books.Repositories;
using Books.Data;
using Books.Models;
using FluentAssertions;

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
            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task InsertBook_AddsBookToDatabase()
        {
            // Act
            await _repository.InsertBook(BooksTestData.Book1);

            // Assert
            _context.Books.Should().Contain(BooksTestData.Book1);
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
            result.Should().BeEquivalentTo(BooksTestData.Book1);
        }
        [Fact]
        public async Task GetBookByISBN_ReturnsNull_WhenBookNotFound()
        {
            // Arrange
            await _repository.InsertBook(BooksTestData.Book1);

            // Act
            var result = await _repository.GetBookByISBN("456"); // ISBN that does not exist

            // Assert
            result.Should().BeNull();
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
            result.Should().BeEquivalentTo(BooksTestData.Book1);
        }

        [Fact]
        public async Task GetBookById_ReturnsNull_WhenBookNotFound()
        {
            // Arrange
            await _repository.InsertBook(BooksTestData.Book1);

            // Act
            var result = await _repository.GetBookById("ID2"); // ID that does not exist

            // Assert
            result.Should().BeNull();
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
            updatedBook.Should().NotBeNull();
            updatedBook!.Title.Should().Be("Updated Title");
        }

        [Fact]
        public async Task UpdateBook_ThrowsArgumentException_WhenBookNotFound()
        {
            // Arrange
            var bookToUpdate = BooksTestData.Book1;

            // Act and Assert

            await _repository.Invoking(y => y.UpdateBook(bookToUpdate))
                             .Should().ThrowAsync<ArgumentException>();
        }
    }
}