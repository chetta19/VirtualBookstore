using Moq;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using Books.Interfaces;
using FluentAssertions;

namespace Books.Test.Unit
{
    public class BooksControllerTest
    {
        private Mock<IBookRepository> _mockRepo;
        private BooksController _controller;

        public BooksControllerTest()
        {
            _mockRepo = new Mock<IBookRepository>();
            _controller = new BooksController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllBooks_ReturnsAllBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                BooksTestData.Book1,
                BooksTestData.Book2
            };
            _mockRepo.Setup(repo => repo.GetAllBooks()).ReturnsAsync(books);

            // Act
            var result = await _controller.Get();

            // Assert
            var actionResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var model = actionResult.Value.Should().BeAssignableTo<IEnumerable<Book>>().Subject;
            model.Count().Should().Be(2);
        }
        [Fact]
        public async Task Post_CreatesBook_ReturnsCreatedAtActionResult()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.InsertBook(BooksTestData.Book1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(BooksTestData.Book1);

            // Assert
            _mockRepo.Verify(repo => repo.InsertBook(BooksTestData.Book1), Times.Once);
            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            var returnValue = createdAtActionResult.Value.Should().BeOfType<Book>().Subject;
            returnValue.Should().Be(BooksTestData.Book1);
        }
        [Fact]
        public async Task Put_UpdatesBook_ReturnsOkObjectResult()
        {
            // Arrange
            var bookToUpdate = BooksTestData.Book1;
            bookToUpdate.Title = "Updated Title";
            _mockRepo.Setup(repo => repo.UpdateBook(bookToUpdate)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Put(bookToUpdate);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateBook(bookToUpdate), Times.Once);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnValue = okResult.Value.Should().BeOfType<Book>().Subject;
            returnValue.Should().Be(bookToUpdate);
        }
    }
}