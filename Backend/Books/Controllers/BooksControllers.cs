using Books.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksControllers : ControllerBase
    {
        private IBookRepository bookRepository;

        public BooksControllers(IBookRepository bookRepository)
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