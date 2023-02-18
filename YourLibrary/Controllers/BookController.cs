using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Enums;

namespace YourLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("GetAllBooks")]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetAllBooksAsync(int pageNumber = 1, int pageSize = 10, string sortColumn = "Title", SortDirection sortDirection = SortDirection.Ascending)
        {
            var books = await _bookService.GetAllBooksAsync(pageNumber, pageSize, sortColumn, sortDirection);
            return Ok(books);
        }

        [HttpGet("GetBookById/{id}")]
        public async Task<ActionResult> GetBookByIdAsync(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return Ok(book);

        }
        [HttpPost("InsertBookWithAuthors")]
        public async Task<ActionResult> InsertBookWithAuthorAsync([FromBody] BookModel book)
        {

            _bookService.InsertBookWithAuthorAsync(book);
            
            return Ok();
        }
        [HttpPut("UpdateBookAsync/{id}")]
        public async Task<ActionResult> UpdateBookByIdAsync(int id, BookModel book)
        {

            var updatedBook = _bookService.UpdateBookByIdAsync(id, book);

            return Ok(updatedBook);
        }
        [HttpDelete("DeleteBookAsync/{id}")]
        public async Task<ActionResult> DeleteBookAsync(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if(result == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
