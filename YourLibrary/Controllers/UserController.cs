using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace YourLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("current")]
        [Authorize] // Add authorization attribute to ensure only authenticated users can access this endpoint
        public async Task<IActionResult> GetCurrentUser()
        {
           // Retrieve the authenticated user's information from the JWT token's claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var firstName = User.FindFirst(JwtRegisteredClaimNames.GivenName)?.Value;
            var lastName = User.FindFirst(JwtRegisteredClaimNames.FamilyName)?.Value;
            var email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
            var dateOfBirth = User.FindFirst(JwtRegisteredClaimNames.Birthdate)?.Value;

            // Create a response object with the user information
            var currentUser = new CurrentUserModel
            {
                Id = int.Parse(userId),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                DateOfBirth = !string.IsNullOrEmpty(dateOfBirth) ? DateTime.Parse(dateOfBirth) : null
            };

            return Ok(currentUser);
                }
            }
}
