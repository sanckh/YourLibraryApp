using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Enums;
using ApplicationCore.Contracts.Repository;

namespace YourLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
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

        [HttpPost("InsertBookWithAuthors")]
        public async Task<ActionResult> InsertBookWithAuthorAsync([FromBody] BookWithAuthorsModel book)
        {
            var authorIds = new List<int>();
            foreach(var authorName in book.AuthorNames)
            {
                var author = await _authorService.GetAuthorByNameAsync(authorName);
                if(author == null)
                {
                    // author doesn't exist, insert it into the database
                    var authorModel = new AuthorModel { FullName = authorName };
                    var authorId = await _authorService.InsertAuthorAsync(authorModel);
                    authorIds.Add(authorId);
                }
                else
                {
                    authorIds.Add(author.Id);
                }
            }
            book.AuthorIds = authorIds;
            await _bookService.InsertBookWithAuthorAsync(book);
            return Ok();
        }
    }
}
