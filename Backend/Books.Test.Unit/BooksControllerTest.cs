using Moq;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using Books.Interfaces;

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
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Book>>(actionResult.Value);
            Assert.Equal(2, model.Count());
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
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Book>(createdAtActionResult.Value);
            Assert.Equal(BooksTestData.Book1, returnValue);
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
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(bookToUpdate, returnValue);
        }
    }
}