using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Enums;
using ApplicationCore.Contracts.Repository;
using Infrastructure.Services;

namespace YourLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        //TODO: Rename this controller to UserBooksController.cs -- This Controller will be dealing with the books in the users library and shelves. 
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IAuthorRepository _authorRepository;

        public BookController(IBookService bookService, IAuthorService authorService, IAuthorRepository authorRepository)
        {
            _bookService = bookService;
            _authorService = authorService;
            _authorRepository = authorRepository;
        }

        [HttpGet("GetAllBooks")]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetAllBooksAsync(int pageNumber, int pageSize, int userId, string sortColumn, SortDirection sortDirection)
        {
            var books = await _bookService.GetAllBooksAsync(pageNumber, pageSize, userId, sortColumn, sortDirection);
            return Ok(books);
        }

        [HttpGet("GetBookById/{id}")]
        public async Task<ActionResult> GetBookByIdAsync(int bookId, int userId)
        {
            var book = await _bookService.GetBookByIdAsync(bookId, userId);
            return Ok(book);

        }

        [HttpPut("updatebook/{bookId}")]
        public async Task<ActionResult> UpdateBookByIdAsync(int bookId, int userId, [FromBody] UserBookModel updatedBook)
        {

            var result = _bookService.UpdateBookAsync(bookId, userId, updatedBook);

            return Ok(result);
        }
        [HttpDelete("DeleteBookAsync/{id}")]
        public async Task<ActionResult> DeleteBookAsync(int userId, int bookId)
        {
            var result = await _bookService.DeleteBookAsync(userId, bookId);
            if(result == 0)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet("recentlyadded")]
        public async Task<IActionResult> GetRecentlyAddedBooksAsync(int userId)
        {
            var days = 30; //Number of days for recent additions
            var books = await _bookService.GetRecentlyAddedBooksAsync(days, userId);
            return Ok(books);
        }

        [HttpGet("unread")]
        public async Task<IActionResult> GetUnreadBooksAsync(int userId)
        {
            var books = await _bookService.GetUnreadBooksAsync(userId);
            return Ok(books);
        }

        [HttpPost("add-to-library")]
        public async Task<IActionResult> AddBookToUserLibraryAsync([FromRoute] int userId, [FromBody] BookModel newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userBook = await _bookService.AddBookToUserLibraryAsync(userId, newBook);
                return Ok(userBook);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("add-book")]
        public async Task<IActionResult> AddBookAsync(BookModel model)
        {
            var bookId = await _bookService.AddBookAsync(model);
            return Ok(bookId);
        }

    }
}
