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
        private readonly ILogger<UserController> logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpGet("current")]
        [Authorize] // Add authorization attribute to ensure only authenticated users can access this endpoint
        public async Task<ActionResult<CurrentUserModel>> GetCurrentUser()
        {
            var currentUserModel = await userService.GetCurrentUser();

            if (currentUserModel == null)
            {
                return NotFound("User not found");
            }

            return Ok(currentUserModel);
            //// Retrieve the authenticated user's information from the JWT token's claims
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var firstName = User.FindFirstValue(JwtRegisteredClaimNames.GivenName);
            //var lastName = User.FindFirstValue(JwtRegisteredClaimNames.FamilyName);
            //var email = User.FindFirstValue(JwtRegisteredClaimNames.Email);
            //var dateOfBirthClaim = User.FindFirst(JwtRegisteredClaimNames.Birthdate);

            //var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            //DateTime? dateOfBirth = null;
            //if (dateOfBirthClaim != null && DateTime.TryParse(dateOfBirthClaim.Value, out var parsedDateOfBirth))
            //{
            //    dateOfBirth = parsedDateOfBirth;
            //}

            //// Create a response object with the user information
            //var currentUser = new CurrentUserModel
            //{
            //    Id = int.Parse(userId),
            //    FirstName = firstName,
            //    LastName = lastName,
            //    Email = email,
            //    DateOfBirth = dateOfBirth
            //};

            //logger.LogInformation($"JWT Token: {token}");

            //return Ok(currentUser);
        }
    }
}
