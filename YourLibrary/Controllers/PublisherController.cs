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
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpPost("AddPublisher")]
        public async Task<ActionResult> AddPublisherAsync([FromBody] PublisherModel publisher)
        {
            await _publisherService.AddPublisherAsync(publisher);

            return Ok();
        }
    }
}
