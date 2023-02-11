using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;
using ApplicationCore.Contracts.Services;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetAllBooksAsync()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookByIdAsync(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return Ok(book);

        }
        [HttpPost]
        public async Task<ActionResult> InsertBookAsync(BookModel model)
        {
            var result = await _bookService.InsertBookAsync(model);
            if(result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBookAsync(int id, BookModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var result = await _bookService.UpdateBookAsync(model);
            if(result == 0)
            {
                return NotFound();
            }

            return Ok();
        }
        [HttpDelete("{id}")]
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
