using Books.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Books
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookRepository bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var books = bookRepository.GetAllBooks();
            return new OkObjectResult(books);
        }
    }
}