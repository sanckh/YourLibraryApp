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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("InsertAuthor")]
        public async Task<ActionResult<int>> InsertAuthorAsync([FromBody]AuthorModel author)
        {
            var id = await _authorService.InsertAuthorAsync(author);
            return Ok(id);
        }

        [HttpDelete("DeleteAuthor/{id}")]
        public async Task<ActionResult<int>> DeleteAuthorAsync(int id)
        {
            var result = await _authorService.DeleteAuthorAsync(id);

            if(result == 0)
            {
                return NotFound();
            }
            return Ok(id);
        }
    }
}
