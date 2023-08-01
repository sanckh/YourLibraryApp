using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ApplicationCore.Models;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Enums;
using ApplicationCore.Contracts.Repository;

namespace YourLibrary.API.Controllers
{
    public class GoogleBooksController : Controller
    {
        private readonly IGoogleBooksService _googleBooksService;

        public GoogleBooksController(IGoogleBooksService googleBookService)
        {
            _googleBooksService = googleBookService;
        }

        // Endpoint to search for books.
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks(string query, int startIndex = 0)
        {
            var results = await _googleBooksService.SearchBooks(query, startIndex);
            return Ok(results);
        }

        // Endpoint to get book details.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookDetails(int bookId)
        {
            var book = await _googleBooksService.GetBookDetails(bookId);
            return Ok(book);
        }

    }
}
